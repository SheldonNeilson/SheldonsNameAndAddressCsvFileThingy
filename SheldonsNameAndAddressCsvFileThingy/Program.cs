using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SheldonsNameAndAddressCsvFileThingy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inputFilePath = null, outputFolderPath = null;
                if (args == null || args.Length == 0)
                    throw new ArgumentException("Input file path required");

                inputFilePath = args[0];
                if (args.Length > 1)
                    outputFolderPath = args[1];

                Program program = new Program(inputFilePath, outputFolderPath);
                program.Run();                
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                Console.WriteLine();
                Console.WriteLine("Usage: SheldonsNameAndAddressCsvFileThingy.exe <InputFilePath> [OutputFolderPath]");
                Console.WriteLine("e.g: SheldonsNameAndAddressCsvFileThingy.exe c:\\tmp\\data.csv c:\\tmp\\output");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private FileInfo inputFile;
        private FileInfo nameIncidenceOutputFile;
        private FileInfo addressOutputFile;

        internal Program(string inputFilePath, string outputFolderPath = null)
        {
            inputFile = new FileInfo(inputFilePath);
            if (!inputFile.Exists)
                throw new FileNotFoundException("The input file specified does not exist");

            outputFolderPath = outputFolderPath ?? inputFile.DirectoryName;

            DirectoryInfo outputFolderInfo = new DirectoryInfo(outputFolderPath);
            if (!outputFolderInfo.Exists)
                outputFolderInfo.Create();

            string inputFileName = Path.GetFileNameWithoutExtension(inputFile.Name);

            string nameIncidenceOutputFileName = string.Format("{0} - First & Last Name Incidence.txt", inputFileName);
            nameIncidenceOutputFile = new FileInfo(Path.Combine(outputFolderInfo.FullName, nameIncidenceOutputFileName));

            string addressOutputFileName = string.Format("{0} - Addresses.txt", inputFileName);
            addressOutputFile = new FileInfo(Path.Combine(outputFolderInfo.FullName, addressOutputFileName));
        }

        internal void Run()
        {
            ReportProgress("Reading data from: {0}", inputFile.FullName);

            Stopwatch stopwatch = new Stopwatch();            
            stopwatch.Start();

            IFileReader<Person> personFileReader = new PersonFileReader(new CsvPersonParser());            
            Person[] people = personFileReader.Read(inputFile.FullName, skip: 1).ToArray();

            IFileWriter fileWriter = new PlainTextFileWriter();

            IEnumerable<IEnumerableIncidence.Result<string>> nameIncidence = GetSortedNameIncidence(people);
            OutputNameIncidence(nameIncidence, fileWriter, nameIncidenceOutputFile.FullName);
            
            IEnumerable<Address> addresses = GetSortedAddresses(people);
            OutputAddresses(addresses, fileWriter, addressOutputFile.FullName);
            

            stopwatch.Stop();
                        
            ReportProgress("First & last name incidence saved as: {0}", nameIncidenceOutputFile.FullName);
            ReportProgress("Addresses saved as: {0}", nameIncidenceOutputFile.FullName);
            ReportProgress("Total running time: {0}", stopwatch.Elapsed);
        }

        private IEnumerable<IEnumerableIncidence.Result<string>> GetSortedNameIncidence(IEnumerable<Person> people)
        {
            return people.GetIncidence(person => person.FirstName)
                .Union(people.GetIncidence(person => person.LastName))
                .OrderByDescending(result => result.Count)
                .ThenBy(result => result.Value);
        }

        private void OutputNameIncidence(IEnumerable<IEnumerableIncidence.Result<string>> incidenceResult, IFileWriter fileWriter, string filePath)
        {
            fileWriter.Write(incidenceResult, i => string.Format("{0} {1}", i.Value, i.Count), filePath);
        }

        private IEnumerable<Address> GetSortedAddresses(IEnumerable<Person> people)
        {
            return people.Select(person => person.Address).OrderBy(address => address.StreetName);
        }

        private void OutputAddresses(IEnumerable<Address> addresses, IFileWriter fileWriter, string filePath)
        {
            fileWriter.Write(addresses, address => address.ToString(), filePath);
        }

        private void ReportProgress(string format, params object[] arg)
        {
            Console.WriteLine();
            Console.WriteLine(format, arg);
        }
    }
}
