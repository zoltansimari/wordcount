using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCounter.Domain
{
    /// <summary>
    /// Turns lines of text into dictionary of words and their counts.
    /// Note: Some of the logic here is specific to latin alphabets, and is untested for other languages
    /// </summary>
    public class DictionaryProducer : IDictionaryProducer
    {
        public IDictionary<string, int> CreateDictionary(IEnumerable<string> lines, char[] specialCharacters)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));
            if (specialCharacters == null)
                throw new ArgumentNullException(nameof(specialCharacters));

            if (!lines.Any())
                throw new ArgumentException("You need at least one line of text to analyse");
            if (!specialCharacters.Any())
                throw new ArgumentException("You need at least one special character (separator) to analyse");

            var countsPerWord = new Dictionary<string, int>();
            foreach (var line in lines)
            {
                var wordsInTheLine = line.Split(specialCharacters, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in wordsInTheLine)
                {
                    IncreaseWordCounter(countsPerWord, word.ToLowerInvariant());
                }
            }

            return countsPerWord;
        }

        private void IncreaseWordCounter(IDictionary<string, int> dictionary, string keyword)
        {
            if (!dictionary.ContainsKey(keyword))
            {
                dictionary.Add(keyword, 0);
            }
            dictionary[keyword]++;
        }
    }
}
