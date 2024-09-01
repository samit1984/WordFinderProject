using System;
using System.Collections.Generic;
using WordFinderLibrary;
using WordFinderLibrary.Factories;
using WordFinderLibrary.Strategies;
using WordFinderLibrary.TrieNode;

namespace WordFinderConsoleApp
{
    class Program
    {
        /// <summary>
        /// •	Matrix Definition: Defines a matrix of characters as a list of strings.
        ///•	Words Definition: Defines a list of words to search for in the matrix..
        ///•	Default Strategy: Uses the default Brute Force strategy to find words in the matrix and prints the found words.
        /// •	DFS Strategy: Uses the strategy factory to create a DFS strategy, sets it in the WordFinder, finds the words, and prints the found words.
        ///•	Trie Strategy: Uses the strategy factory to create a Trie strategy, sets it in the WordFinder, finds the words, and prints the found words.
        /// </summary>
        static void Main()
        {
            // Define the matrix of characters
            var matrix = new List<string>
            {
                "axtqwxs",
                "lqwerta",
                "easdfgm",
                "xghjkti",
                "johnmna",
                "mikeopa",
                "sarasta",
                "daveuva",
                "lizwxya"
            };
            
            // Define the list of words to search for
            var words = new List<string> { "alex", "john", "mike", "sara", "dave", "liz", "samit" };

            // Using default Brute Force strategy
            var wordFinder = new WordFinder(matrix);
            var foundWordsDefault = wordFinder.Find(words);
            Console.WriteLine("Default (Brute Force) Strategy: " + string.Join(", ", foundWordsDefault));

            // Using strategy factory to inject DFS strategy
            var dfsStrategy = SearchStrategyFactory.CreateStrategy(typeof(DFSSearchStrategy));
            wordFinder.SetSearchStrategy(dfsStrategy);
            var foundWordsDFS = wordFinder.Find(words);
            Console.WriteLine("DFS Strategy: " + string.Join(", ", foundWordsDFS));

            // Using strategy factory to inject Trie strategy
            var trieStrategy = SearchStrategyFactory.CreateStrategy(typeof(TrieSearchStrategy));
            wordFinder.SetSearchStrategy(trieStrategy);
            var foundWordsTrie = wordFinder.Find(words);
            Console.WriteLine("Trie Strategy: " + string.Join(", ", foundWordsTrie));
            Console.ReadKey();
        }
    }
}
