using System.Collections.Generic;
using System.IO;

namespace WordCounter.Data
{
    public class FileLinesProvider : ILinesProvider
    {
        public IEnumerable<string> GetLines(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            return File.ReadAllLines(filePath);
        }
    }
}
