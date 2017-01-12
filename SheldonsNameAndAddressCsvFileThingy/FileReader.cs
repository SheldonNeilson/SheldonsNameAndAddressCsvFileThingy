using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SheldonsNameAndAddressCsvFileThingy
{
    public abstract class FileReader<T> : IFileReader<T>
    {
        public IParser<T, string> Parser { get; private set; }
        
        public FileReader(IParser<T, string> parser)
        {
            this.Parser = parser;
        }

        public IEnumerable<T> Read(string filePath, int? skip = null, int? take = null)
        {
            var raw = ReadFile(filePath);
            if (skip.HasValue)
            {
                if (skip.Value > 0)
                    raw = raw.Skip(skip.Value);
                else
                    throw new ArgumentOutOfRangeException("skip", "The value provided for the skip argument must be > 0");
            }
            if (take.HasValue)
            {
                if (take.Value > 1)
                    raw = raw.Take(take.Value);
                else
                    throw new ArgumentOutOfRangeException("take", "The value provided for the take argument must be > 0");
            }

            var cooked = raw.Select(line => Parser.Parse(line));

            return cooked;
        }

        protected virtual IEnumerable<string> ReadFile(string filePath)
        {
            return File.ReadLines(filePath);
        }
    }
}
