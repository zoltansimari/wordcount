using System;
using System.Collections.Generic;
using System.Linq;
using WordCounter.Domain.Languages;

namespace WordCounter.Domain
{
    public class WordStatisticsService
    {
        private readonly IDictionaryProducer dictionaryProducer;
        private readonly LanguageDefinition languageDefinition;

        public WordStatisticsService(IDictionaryProducer dictionaryProducer, LanguageDefinition languageDefinition)
        {
            this.dictionaryProducer = dictionaryProducer;
            this.languageDefinition = languageDefinition;
        }

        public IDictionary<string, int> GetTopTen(IEnumerable<string> lines)
        {
            var fullDictionary = dictionaryProducer.CreateDictionary(lines, languageDefinition.SpecialCharacters);
            return fullDictionary.OrderByDescending(kvp => kvp.Value).Take(10).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
