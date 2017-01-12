using System.Collections.Generic;

namespace SheldonsNameAndAddressCsvFileThingy
{
    public interface IFileReader<T>
    {
        IEnumerable<T> Read(string filePath, int? skip = null, int? take = null);        
    }
}