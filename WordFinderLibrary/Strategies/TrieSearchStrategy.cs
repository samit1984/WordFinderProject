using System.Collections.Generic;
using WordFinderLibrary.Interfaces;
using WordFinderLibrary.TrieNode;

namespace WordFinderLibrary.Strategies
{
    public class TrieSearchStrategy : ISearchStrategy
    {
        private Trie _trie = new Trie();
        private readonly int[][] _directions =
        [
            [0, 1], // Move right
            //[0, -1], // Move left
            [1, 0], // Move down
            // [-1, 0] // Move up
        ];
        private readonly HashSet<string> _wordsInTrie = [];

        private void DfsSearch(char[,] matrix, TrieNode.TrieNode trie, int i, int j)
        {
            if (i < 0 || j < 0 || i >= matrix.GetLength(0) || j >= matrix.GetLength(1)) return;

            var c = matrix[i, j];
            if (c == '#' || !trie.Children.ContainsKey(c)) return;

            trie = trie.Children[c];

            if (trie.Word != null)
            {
                _wordsInTrie.Add(trie.Word);
            }

            matrix[i, j] = '#'; // Mark as visited

            foreach (var dir in _directions)
            {
                DfsSearch(matrix, trie, i + dir[0], j + dir[1]);
            }

            matrix[i, j] = c; // Revert back to original character
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
