using System;

namespace SheldonsNameAndAddressCsvFileThingy
{
    public sealed class CsvPersonParser : IParser<Person,string>
    {
        private const string EXCEPTION_INVALID_PERSON = "Invalid person. People must be provided in the format <FirstName>,<LastName>,<Address>,<PhoneNumber>";

        private Func<string, Address> ParseAddress;
        private Func<string, string> ParsePhoneNumber;

        public CsvPersonParser()
        {
            ParseAddress = new AddressStringParser().Parse;
            ParsePhoneNumber = new PhoneNumberStringParser().Parse;
        }

        public Person Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            string[] parts = input.Split(new char[] { ',' }, 4);

            if (parts.Length != 4 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]) || string.IsNullOrWhiteSpace(parts[2]) || string.IsNullOrWhiteSpace(parts[3]))
                throw new FormatException(EXCEPTION_INVALID_PERSON);

            Person person = new Person()
            {
                FirstName = parts[0].Trim(),
                LastName = parts[1].Trim(),
                Address = ParseAddress(parts[2]),
                PhoneNumber = ParsePhoneNumber(parts[3]),
            };
            return person;
        }
    }
}
