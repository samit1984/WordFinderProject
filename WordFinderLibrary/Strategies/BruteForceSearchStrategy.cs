using WordFinderLibrary.Interfaces;

namespace WordFinderLibrary.Strategies
{
    /// <summary>
    /// Implements a brute force search strategy to find words in a character matrix.
    /// 
    /// Time Complexity:
    /// - SearchWord: O(N * M * L), where N is the number of rows, M is the number of columns, and L is the length of the word.
    /// - FindWords: O(W * N * M * L), where W is the number of words, N is the number of rows, M is the number of columns, and L is the length of the word.
    /// 
    /// Memory Complexity:
    /// - O(1) for search operations as no additional memory is used that scales with input size.
    /// - O(W) for storing the found words in a dictionary, where W is the number of words.
    /// </summary>
    public class BruteForceSearchStrategy : ISearchStrategy
    {
        /// <summary>
        /// Searches for a word in the matrix starting from any position.
        /// </summary>
        /// <param name="matrix">The character matrix to search within.</param>
        /// <param name="word">The word to search for.</param>
        /// <returns>True if the word is found, otherwise false.</returns>
        public bool SearchWord(char[,] matrix, string word)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);

            // Iterate through each cell in the matrix
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    // Check if the word can be found starting from this position
                    if (SearchFromPosition(matrix, word, i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Searches for a word starting from a specific position in the matrix.
        /// </summary>
        /// <param name="matrix">The character matrix to search within.</param>
        /// <param name="word">The word to search for.</param>
        /// <param name="row">The starting row position.</param>
        /// <param name="col">The starting column position.</param>
        /// <returns>True if the word is found, otherwise false.</returns>
        private bool SearchFromPosition(char[,] matrix, string word, int row, int col)
        {
            // Check both horizontally and vertically from the starting position
            return SearchHorizontally(matrix, word, row, col) || SearchVertically(matrix, word, row, col);
        }

        /// <summary>
        /// Searches for a word horizontally from a specific position in the matrix.
        /// </summary>
        /// <param name="matrix">The character matrix to search within.</param>
        /// <param name="word">The word to search for.</param>
        /// <param name="row">The starting row position.</param>
        /// <param name="col">The starting column position.</param>
        /// <returns>True if the word is found, otherwise false.</returns>
        private bool SearchHorizontally(char[,] matrix, string word, int row, int col)
        {
            // Check if the word fits horizontally from the starting position
            if (col + word.Length > matrix.GetLength(1)) return false;

            // Compare each character in the word with the corresponding character in the matrix
            for (var i = 0; i < word.Length; i++)
            {
                if (matrix[row, col + i] != word[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Searches for a word vertically from a specific position in the matrix.
        /// </summary>
        /// <param name="matrix">The character matrix to search within.</param>
        /// <param name="word">The word to search for.</param>
        /// <param name="row">The starting row position.</param>
        /// <param name="col">The starting column position.</param>
        /// <returns>True if the word is found, otherwise false.</returns>
        private bool SearchVertically(char[,] matrix, string word, int row, int col)
        {
            // Check if the word fits vertically from the starting position
            if (row + word.Length > matrix.GetLength(0)) return false;

            // Compare each character in the word with the corresponding character in the matrix
            for (var i = 0; i < word.Length; i++)
            {
                if (matrix[row + i, col] != word[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Finds the specified words in the given character matrix.
        /// </summary>
        /// <param name="matrix">The character matrix to search within.</param>
        /// <param name="words">The list of words to find in the matrix.</param>
        /// <returns>A dictionary where the keys are the found words and the values are their respective counts.</returns>
        public Dictionary<string, int> FindWords(char[,] matrix, IList<string> words)
        {
            var foundWords = new Dictionary<string, int>();

            // Iterate through each word to search for
            foreach (var word in words)
            {
                // If the word is already found, increment its count
                if (foundWords.ContainsKey(word))
                {
                    foundWords[word]++;
                }
                // If the word is found in the matrix, add it to the dictionary
                else if (SearchWord(matrix, word))
                {
                    foundWords[word] = 1;
                }
            }
            return foundWords;
        }
    }
}
