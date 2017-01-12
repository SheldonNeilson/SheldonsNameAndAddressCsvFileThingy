using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SheldonsNameAndAddressCsvFileThingy.Tests
{
    [TestClass]
    public class CsvPersonParserTests : StringParserTester<Person>
    {
        public CsvPersonParserTests() : base(new CsvPersonParser())
        {
        }
        
        [TestMethod]
        public void TestValidArgument()
        {
            Person person = Parser.Parse("Jane,Doe,1 Long Street,0111231234");
            Assert.AreEqual("Jane", person.FirstName);
            Assert.AreEqual("Doe", person.LastName);
            Assert.AreEqual((uint)1, person.Address.StreetNumber);
            Assert.AreEqual("Long Street", person.Address.StreetName);
            Assert.AreEqual("0111231234", person.PhoneNumber);

            Person person2 = Parser.Parse("John,van der Deer,99 A Multi String Street Name,+27 82 123 1234");
            Assert.AreEqual("John", person2.FirstName);
            Assert.AreEqual("van der Deer", person2.LastName);
            Assert.AreEqual((uint)99, person2.Address.StreetNumber);
            Assert.AreEqual("A Multi String Street Name", person2.Address.StreetName);
            Assert.AreEqual("+27 82 123 1234", person2.PhoneNumber);
        }


        [TestMethod]
        public void TestFormatPadding()
        {
            Person person = Parser.Parse(" Jane , Doe , 1 Long Street , +27111231234 ");
            Assert.AreEqual("Jane", person.FirstName);
            Assert.AreEqual("Doe", person.LastName);
            Assert.AreEqual((uint)1, person.Address.StreetNumber);
            Assert.AreEqual("Long Street", person.Address.StreetName);
            Assert.AreEqual("+27111231234", person.PhoneNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException),AllowDerivedTypes =true)]
        public void TestTooFewCommaSeparatedValues()
        {
            Person person = Parser.Parse("Jane,Doe,0111231234");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestTooManyCommaSeparatedValues()
        {
            Person person = Parser.Parse("Jane,Doe,1 Long Street,0111231234,Something else");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestEmptyFirstName()
        {
            Parser.Parse(" ,Doe,1 Long Street,0111231234");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestEmptyLastName()
        {
            Parser.Parse("Jane, ,1 Long Street, 0111231234");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestEmptyAddress()
        {
            Parser.Parse("Jane,Doe, , 0111231234");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestEmptyPhoneNumber()
        {
            Parser.Parse("Jane,Doe,1 Long Street,");
        }

    }
}
