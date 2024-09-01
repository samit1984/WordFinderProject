using WordFinderLibrary.Interfaces;

namespace WordFinderLibrary.Strategies
{
    public class DFSSearchStrategy : ISearchStrategy
    {
        private readonly int[][] _directions =
        [
            [0, 1], // Move right
            //[0, -1], // Move left
            [1, 0], // Move down
            //[-1, 0] // Move up
        ];

        public bool SearchWord(char[,] matrix, string word)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (DFS(matrix, word, i, j, 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DFS(char[,] board, string word, int r, int c, int index)
        {
            if (index == word.Length) return true;
            if (r < 0 || c < 0 || r >= board.GetLength(0) || c >= board.GetLength(1) || board[r, c] != word[index]) return false;

            char temp = board[r, c];
            board[r, c] = '#'; // Mark as visited

            bool found = false;
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

        public Dictionary<string, int> FindWords(char[,] matrix, IList<string> words)
        {
            var foundWords = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (foundWords.ContainsKey(word))
                {
                    foundWords[word]++;
                }
                else if (SearchWord(matrix, word))
                {
                    foundWords[word] = 1;
                }
            }
            return foundWords;
        }
    }
}
