using System.Collections.Generic;
using System;

namespace SheldonsNameAndAddressCsvFileThingy
{
    public interface IFileWriter
    {
        void Write<T>(IEnumerable<T> output, Func<T, string> selector, string filePath);
    }
}