using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SheldonsNameAndAddressCsvFileThingy.Tests
{
    public class StringParserTester<R>
    {
        public IParser<R, string> Parser { get; private set; }

        public StringParserTester(IParser<R, string> parser)
        {
            Parser = parser;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TestNullArgument()
        {
            Parser.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestEmptyArgument()
        {
            Parser.Parse("");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), AllowDerivedTypes = true)]
        public void TestWhiteSpaceArgument()
        {            
            Parser.Parse(" ");
        }
    }
}