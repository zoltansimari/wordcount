using System.Collections.Generic;

namespace WordCounter.Domain
{
    public interface IDictionaryProducer
    {
        IDictionary<string, int> CreateDictionary(IEnumerable<string> lines, char[] specialCharacters);
    }
}