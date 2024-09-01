using WordFinderLibrary.Interfaces;

namespace WordFinderLibrary.Strategies
{
    /// <summary>
    /// Implements a DFS search strategy to find words in a character matrix.
    /// 
    /// Time Complexity:
    /// - SearchWord: O(N * M * L), where N is the number of rows, M is the number of columns, and L is the length of the word.
    /// 
    /// Memory Complexity:
    /// - O(L) for the recursive DFS call stack, where L is the length of the word.
    /// - O(1) for the iterative DFS as it uses a stack.
    /// </summary>
    public class DFSSearchStrategy : ISearchStrategy
    {
        private readonly int[][] _directions =
        [
            [0, 1], // Move right
            //[0, -1], // Move left
            [1, 0], // Move down
            //[-1, 0] // Move up
        ];

        /// <summary>
        /// Searches for a word in the matrix starting from any position using DFS.
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
                    // Check if the word can be found starting from this position using DFS
                    if (DFS(matrix, word, i, j, 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Performs a recursive DFS to search for the word in the matrix.
        /// </summary>
        /// <param name="board">The character matrix to search within.</param>
        /// <param name="word">The word to search for.</param>
        /// <param name="r">The current row position.</param>
        /// <param name="c">The current column position.</param>
        /// <param name="index">The current index in the word.</param>
        /// <returns>True if the word is found, otherwise false.</returns>
        private bool DFS(char[,] board, string word, int r, int c, int index)
        {
            if (index == word.Length) return true;
            if (r < 0 || c < 0 || r >= board.GetLength(0) || c >= board.GetLength(1) || board[r, c] != word[index]) return false;

            var temp = board[r, c];
            board[r, c] = '#'; // Mark as visited

            var found = false;
            foreach (var dir in _directions)
            {
                if (DFS(board, word, r + dir[0], c + dir[1], index + 1))
                {
                    found = true;
                    break;
                }
            }

            board[r, c] = temp; // Revert back to original character
            return found;
        }

        /// <summary>
        /// Performs an iterative DFS to search for the word in the matrix.
        /// </summary>
        /// <param name="board">The character matrix to search within.</param>
        /// <param name="word">The word to search for.</param>
        /// <param name="r">The starting row position.</param>
        /// <param name="c">The starting column position.</param>
        /// <returns>True if the word is found, otherwise false.</returns>
        private bool IterativeDFS(char[,] board, string word, int r, int c)
        {
            var stack = new Stack<(int, int, int)>();
            stack.Push((r, c, 0));

            while (stack.Count > 0)
            {
                var (x, y, index) = stack.Pop();

                if (index == word.Length) return true;
                if (x < 0 || y < 0 || x >= board.GetLength(0) || y >= board.GetLength(1) || board[x, y] != word[index]) continue;

                var temp = board[x, y];
                board[x, y] = '#'; // Mark as visited

                foreach (var dir in _directions)
                {
                    stack.Push((x + dir[0], y + dir[1], index + 1));
                }

                board[x, y] = temp; // Revert back to original character
            }

            return false;
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
