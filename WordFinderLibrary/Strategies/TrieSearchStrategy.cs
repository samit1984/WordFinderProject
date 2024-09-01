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
        [
            [0, 1], // Move right
            //[0, -1], // Move left
            [1, 0], // Move down
            // [-1, 0] // Move up
        ];
        private readonly HashSet<string> _wordsInTrie = new HashSet<string>();

        private void IterativeDfsSearch(char[,] matrix, TrieNode.TrieNode trie, int i, int j)
        {
            var stack = new Stack<(int, int, TrieNode.TrieNode)>();
            stack.Push((i, j, trie));

            while (stack.Count > 0)
            {
                var (x, y, currentTrie) = stack.Pop();

                if (x < 0 || y < 0 || x >= matrix.GetLength(0) || y >= matrix.GetLength(1)) continue;

                var c = matrix[x, y];
                if (c == '#' || !currentTrie.Children.ContainsKey(c)) continue;

                currentTrie = currentTrie.Children[c];

                if (currentTrie.Word != null)
                {
                    _wordsInTrie.Add(currentTrie.Word);
                }

                matrix[x, y] = '#'; // Mark as visited

                foreach (var dir in _directions)
                {
                    stack.Push((x + dir[0], y + dir[1], currentTrie));
                }

                matrix[x, y] = c; // Revert back to original character
            }
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
                    IterativeDfsSearch(matrix, _trie.GetRoot(), i, j);
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
