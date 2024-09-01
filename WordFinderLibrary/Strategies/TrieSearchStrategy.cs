using System.Collections.Generic;
using WordFinderLibrary.Interfaces;
using WordFinderLibrary.TrieNode;

namespace WordFinderLibrary.Strategies
{
    /// <summary>
    /// TrieSearchStrategy uses a Trie data structure combined with iterative DFS to efficiently find words in a matrix.
    /// 
    /// Time Complexity:
    /// - Building the Trie: O(W * L), where W is the number of words and L is the average length of the words.
    /// - Searching the matrix: O(M * N * L), where M is the number of rows, N is the number of columns, and L is the length of the longest word.
    /// 
    /// Memory Complexity:
    /// - Trie storage: O(W * L), where W is the number of words and L is the average length of the words.
    /// - DFS stack: O(L), where L is the length of the longest word.
    /// </summary>
    public class TrieSearchStrategy : ISearchStrategy
    {
        private Trie _trie = new Trie();

        private readonly int[][] _directions =
        {
            new[] { 0, 1 }, // Move right
            //new[] {0, -1}, // Move left
            new[] { 1, 0 }, // Move down
            //new[] {-1, 0} // Move up
        };
        private readonly HashSet<string> _wordsInTrie = new HashSet<string>();

        private void DfsSearch(char[,] matrix, TrieNode.TrieNode trie, int x, int y)
        {
            if (x < 0 || y < 0 || x >= matrix.GetLength(0) || y >= matrix.GetLength(1))
                return;

            var c = matrix[x, y];
            if (c == '#' || !trie.Children.ContainsKey(c))
                return;

            trie = trie.Children[c];

            if (trie.Word != null)
            {
                _wordsInTrie.Add(trie.Word);
            }

            matrix[x, y] = '#'; // Mark as visited

            foreach (var dir in _directions)
            {
                int newX = x + dir[0];
                int newY = y + dir[1];
                DfsSearch(matrix, trie, newX, newY);
            }

            matrix[x, y] = c; // Revert back to original character
        }

        public Dictionary<string, int> FindWords(char[,] matrix, IList<string> words)
        {
            _trie = new Trie();
            foreach (var word in words.Distinct())
            {
                _trie.Insert(word);
            }

            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    DfsSearch(matrix, _trie.GetRoot(), i, j);
                }
            }

            var foundWords = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (_wordsInTrie.Contains(word))
                {
                    if (!foundWords.TryAdd(word, 1))
                    {
                        foundWords[word]++;
                    }
                }
            }

            return foundWords;
        }
    }
}
