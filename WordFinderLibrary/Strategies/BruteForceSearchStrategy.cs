using WordFinderLibrary.Interfaces;

namespace WordFinderLibrary.Strategies
{
    public class BruteForceSearchStrategy : ISearchStrategy
    {
        public bool SearchWord(char[,] matrix, string word)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (SearchFromPosition(matrix, word, i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool SearchFromPosition(char[,] matrix, string word, int row, int col)
        {
            return SearchHorizontally(matrix, word, row, col) || SearchVertically(matrix, word, row, col);
        }

        private bool SearchHorizontally(char[,] matrix, string word, int row, int col)
        {
            if (col + word.Length > matrix.GetLength(1)) return false;

            for (int i = 0; i < word.Length; i++)
            {
                if (matrix[row, col + i] != word[i])
                {
                    return false;
                }
            }
            return true;
        }

        private bool SearchVertically(char[,] matrix, string word, int row, int col)
        {
            if (row + word.Length > matrix.GetLength(0)) return false;

            for (int i = 0; i < word.Length; i++)
            {
                if (matrix[row + i, col] != word[i])
                {
                    return false;
                }
            }
            return true;
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
