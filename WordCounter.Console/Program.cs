using System;
using WordCounter.Data;
using WordCounter.Domain;
using WordCounter.Domain.Languages;

namespace WordCounter.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Words!");

            var linesProvider = new HttpLinesProvider();
            // This is html and not raw text...
            var lines = linesProvider.GetLines("https://archive.org/stream/TheLordOfTheRing1TheFellowshipOfTheRing/The+Lord+Of+The+Ring+1-The+Fellowship+Of+The+Ring_djvu.txt");
            var wordStatService = new WordStatisticsService(
                new DictionaryProducer(),
                new EnglishDefinition());

            var topTen = wordStatService.GetTopTen(lines);
            var index = 1;

            foreach (var item in topTen)
            {
                Console.WriteLine($"{index++}. word is '{item.Key}' which occurs {item.Value} times");
            }

            Console.ReadLine();
        }
    }
}
