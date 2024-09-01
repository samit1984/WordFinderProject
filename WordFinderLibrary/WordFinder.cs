using System;
using System.Collections.Generic;
using System.Linq;
using WordFinderLibrary.Interfaces;
using WordFinderLibrary.Strategies;

namespace WordFinderLibrary
{
    /// <summary>
    /// Provides functionality to find words in a character matrix using different search strategies.
    /// </summary>
    public class WordFinder
    {
        private const int MaxSize = 64;
        private readonly char[,] _matrix;
        private ISearchStrategy _searchStrategy;

        /// <summary>
        /// Initializes a new instance of the WordFinder class with the given matrix.
        /// </summary>
        /// <param name="matrix">The character matrix to search within.</param>
        /// <exception cref="ArgumentException">Thrown when the matrix size exceeds the maximum allowed size or rows have inconsistent lengths.</exception>
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

        /// <summary>
        /// Sets the search strategy to be used for finding words.
        /// </summary>
        /// <param name="searchStrategy">The search strategy to set.</param>
        public void SetSearchStrategy(ISearchStrategy searchStrategy)
        {
            _searchStrategy = searchStrategy;
        }

        /// <summary>
        /// Finds the top 10 most frequently occurring words in the matrix from the given word stream.
        /// </summary>
        /// <param name="wordstream">The stream of words to search for.</param>
        /// <returns>An enumerable of the top 10 most frequently occurring words.</returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            // Use the search strategy to find words in the matrix
            var foundWords = _searchStrategy.FindWords(_matrix, wordstream.ToList());

            // Order the found words by frequency and return the top 10
            return foundWords.OrderByDescending(kv => kv.Value)
                .Take(10)
                .Select(kv => kv.Key);
        }
    }
}
