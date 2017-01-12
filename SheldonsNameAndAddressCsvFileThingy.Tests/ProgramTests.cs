using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SheldonsNameAndAddressCsvFileThingy.Tests
{
    [TestClass]
    public class ProgramTests
    {
        const string TEST_DATA_FILE_PATH = "Test Data.csv";
        const string INCIDENCE_FILE_PATH_FORMAT = "{0} - First & Last Name Incidence.txt";
        const string ADDRESS_FILE_PATH_FORMAT = "{0} - Addresses.txt";

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void ProgramConstructorNullArgumentTest()
        {
            Program program = new Program(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), AllowDerivedTypes = true)]
        public void ProgramConstructorInvalidArgumentTest()
        {
            Program program = new Program("This is not a valid file path");            
        }

        [TestMethod]
        public void ProgramConstructorValidArgumentTest()
        {            
            Program program = new Program(TEST_DATA_FILE_PATH);
        }

        [TestMethod]
        public void ProgramRunTest()
        {
            string inputFileName = Path.GetFileNameWithoutExtension(TEST_DATA_FILE_PATH);
            string incidenceFilePath = string.Format(INCIDENCE_FILE_PATH_FORMAT, inputFileName);
            string addressFilePath = string.Format(ADDRESS_FILE_PATH_FORMAT, inputFileName);

            if (File.Exists(incidenceFilePath))
                File.Delete(incidenceFilePath);

            if (File.Exists(addressFilePath))
                File.Delete(addressFilePath);

            Program program = new Program(TEST_DATA_FILE_PATH);

            program.Run();
                        
            Assert.IsTrue(File.Exists(incidenceFilePath), "The incidence output file does not exist");
            Assert.IsTrue(File.Exists(addressFilePath), "The address output file does not exist");
        }

        [TestMethod]
        public void ProgramIncidenceOutputTest()
        {
            string inputFileName = Path.GetFileNameWithoutExtension(TEST_DATA_FILE_PATH);
            string incidenceFilePath = string.Format(INCIDENCE_FILE_PATH_FORMAT, inputFileName);

            if (File.Exists(incidenceFilePath))
                File.Delete(incidenceFilePath);
            
            Program program = new Program(TEST_DATA_FILE_PATH);

            program.Run();

            Assert.IsTrue(File.Exists(incidenceFilePath),"The incidence output file does not exist");

            string[] incidenceFileContents = File.ReadAllLines(incidenceFilePath);

            Assert.IsTrue(incidenceFileContents != null && incidenceFileContents.Length == 9, "The incidence output file does contain the correct number of records");

            Assert.AreEqual("Brown 2", incidenceFileContents[0], "The incidence output file does not contain the expected value for record 1");
            Assert.AreEqual("Clive 2", incidenceFileContents[1], "The incidence output file does not contain the expected value for record 2");
            Assert.AreEqual("Graham 2", incidenceFileContents[2], "The incidence output file does not contain the expected value for record 3");
            Assert.AreEqual("Howe 2", incidenceFileContents[3], "The incidence output file does not contain the expected value for record 4");
            Assert.AreEqual("James 2", incidenceFileContents[4], "The incidence output file does not contain the expected value for record 5");
            Assert.AreEqual("Owen 2", incidenceFileContents[5], "The incidence output file does not contain the expected value for record 6");
            Assert.AreEqual("Smith 2", incidenceFileContents[6], "The incidence output file does not contain the expected value for record 7");
            Assert.AreEqual("Jimmy 1", incidenceFileContents[7], "The incidence output file does not contain the expected value for record 8");
            Assert.AreEqual("John 1", incidenceFileContents[8], "The incidence output file does not contain the expected value for record 9");
        }

        [TestMethod]
        public void ProgramAddressOutputTest()
        {
            string inputFileName = Path.GetFileNameWithoutExtension(TEST_DATA_FILE_PATH);
            string addressFilePath = string.Format(ADDRESS_FILE_PATH_FORMAT, inputFileName);

            if (File.Exists(addressFilePath))
                File.Delete(addressFilePath);

            Program program = new Program(TEST_DATA_FILE_PATH);

            program.Run();

            Assert.IsTrue(File.Exists(addressFilePath), "The address output file does not exist");

            string[] addressFileContents = File.ReadAllLines(addressFilePath);

            Assert.IsTrue(addressFileContents != null && addressFileContents.Length == 8, "The address output file does contain the correct number of records");

            Assert.AreEqual("65 Ambling Way", addressFileContents[0], "The address output file does not contain the expected value for record 1");
            Assert.AreEqual("8 Crimson Rd", addressFileContents[1], "The address output file does not contain the expected value for record 2");
            Assert.AreEqual("12 Howard St", addressFileContents[2], "The address output file does not contain the expected value for record 3");
            Assert.AreEqual("102 Long Lane", addressFileContents[3], "The address output file does not contain the expected value for record 4");
            Assert.AreEqual("94 Roland St", addressFileContents[4], "The address output file does not contain the expected value for record 5");
            Assert.AreEqual("78 Short Lane", addressFileContents[5], "The address output file does not contain the expected value for record 6");
            Assert.AreEqual("82 Stewart St", addressFileContents[6], "The address output file does not contain the expected value for record 7");
            Assert.AreEqual("49 Sutherland St", addressFileContents[7], "The address output file does not contain the expected value for record 8");
        }
    }
}
