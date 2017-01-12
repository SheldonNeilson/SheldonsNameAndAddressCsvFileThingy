using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SheldonsNameAndAddressCsvFileThingy.Tests
{
    [TestClass]
    public class AddressStringParserTests : StringParserTester<Address>
    {
        public AddressStringParserTests() : base(new AddressStringParser())
        {
        }
        
        [TestMethod]
        public void TestValidArgument()
        {
            Address address = Parser.Parse("1 Long Street");
            Assert.AreEqual((uint)1, address.StreetNumber);
            Assert.AreEqual("Long Street", address.StreetName);

            Address address2 = Parser.Parse("99 A Multi String Street Name");
            Assert.AreEqual((uint)99, address2.StreetNumber);
            Assert.AreEqual("A Multi String Street Name", address2.StreetName,"A multiple word street was name not parsed correctly");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException),AllowDerivedTypes =true)]
        public void TestInvalidStreetNumber()
        {
            Address address = Parser.Parse("X Long Street");            
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestNegativeStreetNumber()
        {
            Address address = Parser.Parse("-9 Long Street");
        }

        [TestMethod]
        public void TestFormatPadding()
        {
            Address address = Parser.Parse(" 1 Long Street ");
            Assert.AreEqual((uint)1, address.StreetNumber);
            Assert.AreEqual("Long Street", address.StreetName);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestInvalidFormatMissingStreetName()
        {
            Address address = Parser.Parse("1");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestInvalidFormatMissingStreetNumber()
        {
            Address address = Parser.Parse("Long Street");
        }
    }
}
