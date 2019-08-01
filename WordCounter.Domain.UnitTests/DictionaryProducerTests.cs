using FluentAssertions;
using NUnit.Framework;
using System;

namespace WordCounter.Domain.UnitTests
{
    public class DictionaryProducerTests
    {
        private readonly char[] SpecialCharacters = new[] { ' ', '.', ',', '-', '?', '!' };

        [Test]
        public void CreateDictionary_CalledWithNoSpecialCharacters_ShouldThrowException()
        {
            var sampleLines = new[] { "Some Text" };
            var producer = new DictionaryProducer();
            producer.Invoking(p => p.CreateDictionary(sampleLines, null)).Should().Throw<ArgumentNullException>();
            producer.Invoking(p => p.CreateDictionary(sampleLines, new char[0])).Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateDictionary_CalledWithNoTextLines_ShouldThrowException()
        {
            var specialCharacters = new[] { '.' };
            var producer = new DictionaryProducer();
            producer.Invoking(p => p.CreateDictionary(null, specialCharacters)).Should().Throw<ArgumentNullException>();
            producer.Invoking(p => p.CreateDictionary(new string[0], specialCharacters)).Should().Throw<ArgumentException>();
        }

        [TestCase("Hello, this is some text", 5)]
        [TestCase("A - brief - description", 3)]
        [TestCase("Something funny??", 2)]
        public void CreateDictionary_CalledWithCorrectInputs_ShouldReturnAllWordCounts(string firstLine, int expectedWords)
        {
            var producer = new DictionaryProducer();
            var dictionary = producer.CreateDictionary(new[] { firstLine }, SpecialCharacters);
            dictionary.Count.Should().Be(expectedWords);
        }

        [TestCase("Casing Should Not Matter. MaTTER! MatteR?", 4)]
        public void CreateDictionary_CalledWithCorrectInputs_ShouldReturnCorrectCounts(string firstLine, int expectedWords)
        {
            var producer = new DictionaryProducer();
            var dictionary = producer.CreateDictionary(new[] { firstLine }, SpecialCharacters);
            dictionary.Count.Should().Be(expectedWords);
        }

        [Test]
        public void CreateDictionary_CalledWithCorrectInputs_ShouldReturnCorrectDictionary()
        {
            // From the very much recommended piece: https://martinfowler.com/articles/microservice-testing/
            // Expected generated by https://wordcounter.net/

            var sampleText = new[] {"The size of the unit under test is not strictly defined, ",
                "however unit tests are typically written at the class level or around a small group of related classes. ",
                "The smaller the unit under test the easier it is to express the behaviour using a unit test since the ",
                "branch complexity of the unit is lower.",
                "Often, difficulty in writing a unit test can highlight when a module should be broken down into independent ",
                "more coherent pieces and tested individually.Thus, alongside being a useful testing strategy, unit testing ",
                "is also a powerful design tool, especially when combined with test driven development."};

            var producer = new DictionaryProducer();
            var dictionary = producer.CreateDictionary(sampleText, SpecialCharacters);
            dictionary.Should().ContainKey("unit");
            dictionary.Should().ContainKey("test");
            dictionary.Should().ContainKey("under");
            dictionary.Should().ContainKey("when");
            dictionary.Should().ContainKey("testing");
            dictionary["unit"].Should().Be(7);
            dictionary["test"].Should().Be(5);
            dictionary["under"].Should().Be(2);
            dictionary["when"].Should().Be(2);
            dictionary["testing"].Should().Be(2);
        }
    }
}