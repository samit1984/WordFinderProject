using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordFinderLibrary;
using WordFinderLibrary.Factories;
using WordFinderLibrary.Strategies;

namespace WordFinderTests
{
    [TestClass]
    public class WordFinderTests
    {
        [TestMethod]
        public void TestFindWords()
        {
            var matrix = new List<string>
            {
                "chill",
                "coldw",
                "windy",
                "storm",
                "rainy"
            };

            var wordstream = new List<string> { "chill", "cold", "wind", "storm", "rain", "snow" };

            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream).ToList();

            CollectionAssert.AreEquivalent(new List<string> { "chill", "cold", "wind", "storm", "rain" }, result);
        }

        [TestMethod]
        public void TestNoWordsFound()
        {
            var matrix = new List<string>
            {
                "aaaaa",
                "bbbbb",
                "ccccc",
                "ddddd",
                "eeeee"
            };

            var wordstream = new List<string> { "chill", "cold", "wind" };

            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream).ToList();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestMatrixSizeExceedsLimit()
        {
            var matrix = new List<string>(new string[65]);

            for (int i = 0; i < 65; i++)
            {
                matrix[i] = new string('a', 65);
            }

            Assert.ThrowsException<ArgumentException>(() => new WordFinder(matrix));
        }

        [TestMethod]
        public void TestMatrixRowsDifferentLengths()
        {
            var matrix = new List<string>
            {
                "aaaaa",
                "bbbbb",
                "ccccc",
                "dddd",
                "eeeee"
            };

            Assert.ThrowsException<ArgumentException>(() => new WordFinder(matrix));
        }

        [TestMethod]
        public void TestTop10MostRepeatedWords()
        {
            var matrix = new List<string>
            {
                "chill",
                "coldw",
                "windy",
                "storm",
                "rainy"
            };

            var wordstream = new List<string>
            {
                "chill", "cold", "wind", "storm", "rain", "snow",
                "chill", "cold", "wind", "storm", "rain", "snow",
                "chill", "cold", "wind", "storm", "rain", "snow",
                "chill", "cold", "wind", "storm", "rain", "snow",
                "chill", "cold", "wind", "storm", "rain", "snow"
            };

            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream).ToList();

            CollectionAssert.AreEquivalent(new List<string> { "chill", "cold", "wind", "storm", "rain" }, result);
        }

        [TestMethod]
        public void TestBruteForceStrategy()
        {
            var matrix = new List<string>
            {
                "axtqwx",
                "lqwert",
                "easdfg",
                "xghjkl",
                "johnmn",
                "mikeop",
                "sarast",
                "daveuv",
                "lizwxy"
            };

            var words = new List<string> { "alex", "john", "mike", "sara", "dave", "liz" };

            var wordFinder = new WordFinder(matrix);
            var foundWords = wordFinder.Find(words).ToList();
            CollectionAssert.AreEquivalent(new List<string> { "alex", "john", "mike", "sara", "dave", "liz" }, foundWords);
        }

        [TestMethod]
        public void TestDFSStrategy()
        {
            var matrix = new List<string>
            {
                "axtqwx",
                "lqwert",
                "easdfg",
                "xghjkl",
                "johnmn",
                "mikeop",
                "sarast",
                "daveuv",
                "lizwxy"
            };

            var words = new List<string> { "alex", "john", "mike", "sara", "dave", "liz" };

            var wordFinder = new WordFinder(matrix);
            var dfsStrategy = SearchStrategyFactory.CreateStrategy(typeof(DFSSearchStrategy));
            wordFinder.SetSearchStrategy(dfsStrategy);
            var foundWords = wordFinder.Find(words).ToList();
            CollectionAssert.AreEquivalent(new List<string> { "alex", "john", "mike", "sara", "dave", "liz" }, foundWords);
        }

        [TestMethod]
        public void TestTrieStrategy()
        {
            var matrix = new List<string>
            {
                "axtqwx",
                "lqwert",
                "easdfg",
                "xghjkl",
                "johnmn",
                "mikeop",
                "sarast",
                "daveuv",
                "lizwxy"
            };

            var words = new List<string> { "alex", "john", "mike", "sara", "dave", "liz" };

            var wordFinder = new WordFinder(matrix);
            var trieStrategy = SearchStrategyFactory.CreateStrategy(typeof(TrieSearchStrategy));
            wordFinder.SetSearchStrategy(trieStrategy);
            var foundWords = wordFinder.Find(words).ToList();
            CollectionAssert.AreEquivalent(new List<string> { "alex", "john", "mike", "sara", "dave", "liz" }, foundWords);
        }
    }
}
