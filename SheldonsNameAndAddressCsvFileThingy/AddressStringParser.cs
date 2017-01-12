using System;

namespace SheldonsNameAndAddressCsvFileThingy
{
    public sealed class AddressStringParser : IParser<Address, string>
    {
        private const string EXCEPTION_INVALID_ADDRESS = "Invalid address. Addresses must be provided in the format <StreetNumber> <StreetName>";

        public Address Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(input);

            string[] parts = input.Trim().Split(new char[] { ' ' }, 2);

            if (parts == null || parts.Length < 2)
                throw new FormatException(EXCEPTION_INVALID_ADDRESS);

            uint streetNumber;
            if (!UInt32.TryParse(parts[0], out streetNumber) || streetNumber == 0 || string.IsNullOrWhiteSpace(parts[1]))
                throw new FormatException(EXCEPTION_INVALID_ADDRESS);

            Address address = new Address()
            {
                StreetNumber = streetNumber,
                StreetName = parts[1]
            };

            return address;
        }
    }
}
