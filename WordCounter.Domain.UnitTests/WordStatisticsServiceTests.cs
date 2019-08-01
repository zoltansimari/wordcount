using NUnit.Framework;
using Moq;
using WordCounter.Domain.Languages;
using System.Collections.Generic;
using FluentAssertions;

namespace WordCounter.Domain.UnitTests
{
    public class WordStatisticsServiceTests
    {
        private Mock<IDictionaryProducer> dictionaryProducerMock = new Mock<IDictionaryProducer>();
        private LanguageDefinition languageDefinition = Mock.Of<LanguageDefinition>();
        private readonly IDictionary<string, int> result = new Dictionary<string, int>
        {
            { "a", 2 },
            { "b", 2 },
            { "c", 3 },
            { "d", 8 },
            { "e", 21 },
            { "f", 12 },
            { "g", 7 },
            { "h", 4 },
            { "i", 1 },
            { "j", 10 },
            { "k", 13 },
            { "l", 9 },
            { "m", 30 },
        };

        [SetUp]
        public void Setup()
        {
            dictionaryProducerMock
                .Setup(dp => dp.CreateDictionary(It.IsAny<IEnumerable<string>>(), It.IsAny<char[]>()))
                .Returns(result);
        }

        [Test]
        public void GetTop10_ShouldReturnMax10Records()
        {
            var wordStatisticsService = new WordStatisticsService(dictionaryProducerMock.Object, languageDefinition);
            var topTen = wordStatisticsService.GetTopTen(new[] { "something" });
            topTen.Count.Should().Be(10);
        }

        [Test]
        public void GetTop10_ShouldReturnCorrectTopRecords()
        {
            var wordStatisticsService = new WordStatisticsService(dictionaryProducerMock.Object, languageDefinition);
            var topTen = wordStatisticsService.GetTopTen(new[] { "something" });
            topTen.Should().ContainKeys("c", "d", "e", "f", "g", "h", "j", "k", "l", "m");
            topTen.Should().NotContainKeys("a", "b", "i");
        }
    }
}
