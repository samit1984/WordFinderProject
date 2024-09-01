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
            // Test finding words in the matrix and ensuring the correct words are found
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
            // Test the scenario where no words from the word stream are found in the matrix
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
            // Test the scenario where the matrix size exceeds the 64x64 limit
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
            // Test the scenario where the matrix rows have different lengths
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
            // Test finding the top 10 most repeated words in the word stream
            var matrix = new List<string>
            {
                "word1x",
                "word2x",
                "word3x",
                "word4x",
                "word5x",
                "word6x",
                "word7x",
                "word8x",
                "word9x",
                "word10",
                "word11",
                "word12",
                "word13",
                "word14",
                "word15",
                "word16",
                "word17",
                "word18",
                "word19",
                "word20"
            };

            var wordstream = new List<string>
            {
                "word1", "word1", "word1", "word1", "word1", "word1", "word1", "word1", // 8 times
                "word2", "word2", "word2", "word2", "word2", // 5 times
                "word3", "word3", "word3", "word3", // 4 times
                "word4", "word4", "word4", // 3 times
                "word5", "word5", // 2 times
                "word6", "word6", "word6", "word6", "word6", "word6", "word6", "word6", // 8 times
                "word7", "word7", "word7", "word7", "word7", // 5 times
                "word8", "word8", "word8", "word8", // 4 times
                "word9", "word9", "word9", // 3 times
                "word10", "word10", // 2 times
                "word11", "word12", "word13", "word14", "word15", "word16", "word17", "word18", "word19",
                "word20" // 1 time each
            };

            var expectedResults = new List<string>
            {
                "word1", "word6", "word2", "word7", "word3", "word8", "word4", "word9", "word5", "word10"
            };

            var wordFinder = new WordFinder(matrix);

            // Test with default strategy
            var result = wordFinder.Find(wordstream).ToList();
            CollectionAssert.AreEquivalent(expectedResults, result);

            // Test with DFS strategy
            wordFinder.SetSearchStrategy(SearchStrategyFactory.CreateStrategy(typeof(DFSSearchStrategy)));
            result = wordFinder.Find(wordstream).ToList();
            CollectionAssert.AreEquivalent(expectedResults, result);

            // Test with Trie strategy
            wordFinder.SetSearchStrategy(SearchStrategyFactory.CreateStrategy(typeof(TrieSearchStrategy)));
            result = wordFinder.Find(wordstream).ToList();
            CollectionAssert.AreEquivalent(expectedResults, result);
        }

        [TestMethod]
        public void TestBruteForceStrategy()
        {
            // Test the default brute force search strategy
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
            // Test the DFS search strategy
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
            // Test the Trie search strategy
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
