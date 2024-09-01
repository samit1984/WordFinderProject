using System;
using System.Collections.Generic;
using System.Linq;
using WordFinderLibrary.Interfaces;
using WordFinderLibrary.Strategies;

namespace WordFinderLibrary
{
    public class WordFinder
    {
        private const int MaxSize = 64;
        private readonly char[,] _matrix;
        private ISearchStrategy _searchStrategy;

        public WordFinder(IEnumerable<string> matrix)
        {
            var matrixList = matrix.ToList();
            var rows = matrixList.Count;
            var cols = matrixList[0].Length;

            if (rows > MaxSize || cols > MaxSize)
            {
                throw new ArgumentException($"Matrix size cannot exceed {MaxSize}x{MaxSize}");
            }

            if (matrixList.Any(row => row.Length != cols))
            {
                throw new ArgumentException("All strings must contain the same number of characters");
            }

            _matrix = new char[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    _matrix[i, j] = matrixList[i][j];
                }
            }

            // Set default strategy to BruteForceSearchStrategy
            _searchStrategy = new BruteForceSearchStrategy();
        }

        public void SetSearchStrategy(ISearchStrategy searchStrategy)
        {
            _searchStrategy = searchStrategy;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var foundWords= _searchStrategy.FindWords(_matrix, wordstream.ToList());
            return foundWords.OrderByDescending(kv => kv.Value)
                .Take(10)
                .Select(kv => kv.Key);
        }
    }
}
