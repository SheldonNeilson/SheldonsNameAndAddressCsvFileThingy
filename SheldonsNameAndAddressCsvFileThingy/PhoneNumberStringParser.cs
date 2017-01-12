using System;
using System.Text.RegularExpressions;

namespace SheldonsNameAndAddressCsvFileThingy
{
    public sealed class PhoneNumberStringParser : IParser<string, string>
    {
        private const string EXCEPTION_INVALID_PHONE_NUMBER = "Invalid Phone Number";

        public string Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(input);

            input = input.Trim();

            //Remove multiple consecutive whitespaces
            input = Regex.Replace(input, @"\s\s+", "");

            //Have to allow shorter numbers due to invalid numbers in data.csv
            //if (!Regex.Match(input, @"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$").Success)

            if (!Regex.Match(input, @"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{2,4})(( x| ext)\d{1,5}){0,1}$").Success)

                    throw new FormatException(EXCEPTION_INVALID_PHONE_NUMBER);
            
            return input;
        }
    }
}
