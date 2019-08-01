using System.Collections.Generic;

namespace WordCounter.Data
{
    public interface ILinesProvider
    {
        IEnumerable<string> GetLines(string source);
    }
}
