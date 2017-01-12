using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SheldonsNameAndAddressCsvFileThingy.Tests
{
    [TestClass]
    public class PhoneNumberStringParserTests : StringParserTester<string>
    {
        public PhoneNumberStringParserTests() : base(new PhoneNumberStringParser())
        {
        }

        [TestMethod]
        public void TestValidArgument()
        {
            string result = Parser.Parse("+27 11 123 1234");
            Assert.AreEqual(result, "+27 11 123 1234");

            result = Parser.Parse("011 123 1234");
            Assert.AreEqual(result, "011 123 1234");

            result = Parser.Parse("(011) 123-1234");
            Assert.AreEqual(result, "(011) 123-1234");

            result = Parser.Parse("+1-541-754-3010");
            Assert.AreEqual(result, "+1-541-754-3010");

            //data.csv phone numbers are not actually valid. Test the workaround
            result = Parser.Parse("29384857");
            Assert.AreEqual(result, "29384857");

            result = Parser.Parse("31214788");
            Assert.AreEqual(result, "31214788");

            result = Parser.Parse("32114566");
            Assert.AreEqual(result, "32114566");

            result = Parser.Parse("8766556");
            Assert.AreEqual(result, "8766556");

            result = Parser.Parse("29384857");
            Assert.AreEqual(result, "29384857");

            result = Parser.Parse("31214788");
            Assert.AreEqual(result, "31214788");

            result = Parser.Parse("32114566");
            Assert.AreEqual(result, "32114566");

            result = Parser.Parse("8766556");
            Assert.AreEqual(result, "8766556");

        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestShortArgument()
        {
            //string result = Parser.Parse("+27 11 123 12"); data.csv numbers are too short, had to allow them in the parser
            string result = Parser.Parse("+27 11 123");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestLongArgument()
        {
            string result = Parser.Parse("+27 11 123 1234 1234567");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestInvalidCharacters()
        {
            string result = Parser.Parse("+27 11 1Z3 1234");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestMisplacedPlusCharacter()
        {
            string result = Parser.Parse("2+7 11 1Z3 1234");
        }

    }
}
