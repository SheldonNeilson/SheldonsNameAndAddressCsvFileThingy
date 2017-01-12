using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SheldonsNameAndAddressCsvFileThingy.Tests
{
    [TestClass]
    public class IEnumerableIncidenceExtensionTests
    {
        [TestMethod]
        public void IEnumerableIncidenceExtensionTest()
        {
            KeyValuePair<int, string>[] testEnumerable = new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(1,"one"),
                new KeyValuePair<int, string>(2,"two"),
                new KeyValuePair<int, string>(1,"one"),                
                new KeyValuePair<int, string>(2,"two"),
                new KeyValuePair<int, string>(1,"one"),
                new KeyValuePair<int, string>(2,"two"),
                new KeyValuePair<int, string>(1,"one"),
                //
                new KeyValuePair<int, string>(3,"one"),
                new KeyValuePair<int, string>(2,"three")
            };

            IEnumerable<IEnumerableIncidence.Result<int>> keyIncidence = testEnumerable.GetIncidence(r => r.Key);

            IEnumerableIncidence.Result<int> key1Result = keyIncidence.Single(r => r.Value == 1);
            IEnumerableIncidence.Result<int> key2Result = keyIncidence.Single(r => r.Value == 2);

            Assert.AreEqual(4, key1Result.Count);
            Assert.AreEqual(4, key2Result.Count);

            IEnumerable<IEnumerableIncidence.Result<string>> valueIncidence = testEnumerable.GetIncidence(r => r.Value);

            IEnumerableIncidence.Result<string> valueOneResult = valueIncidence.Single(r => r.Value.Equals("one"));
            IEnumerableIncidence.Result<string> valueTwoResult = valueIncidence.Single(r => r.Value.Equals("two"));

            Assert.AreEqual(5, valueOneResult.Count);
            Assert.AreEqual(3, valueTwoResult.Count);
        }
    }
}
