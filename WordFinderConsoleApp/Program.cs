using System;
using System.Collections.Generic;
using WordFinderLibrary;
using WordFinderLibrary.Factories;
using WordFinderLibrary.Strategies;

namespace WordFinderConsoleApp
{
    class Program
    {
        static void Main()
        {
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

            var words = new List<string> { "alex", "john", "mike", "sara", "dave", "liz","samit" };

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
        }
    }
}
