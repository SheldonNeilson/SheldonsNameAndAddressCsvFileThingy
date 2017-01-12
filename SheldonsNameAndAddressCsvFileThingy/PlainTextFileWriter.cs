using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SheldonsNameAndAddressCsvFileThingy
{
    public sealed class PlainTextFileWriter : IFileWriter
    {
        void IFileWriter.Write<T>(IEnumerable<T> output, Func<T, string> selector, string filePath)
        {
            string[] lines = output.Select(selector).ToArray();
            File.WriteAllLines(filePath, lines);
        }        
    }
}