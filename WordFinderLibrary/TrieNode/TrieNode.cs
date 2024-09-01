using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinderLibrary.TrieNode
{
    /// <summary>
    /// Represents a node in a Trie (prefix tree).
    /// 
    /// Time Complexity:
    /// - Insert: O(L), where L is the length of the word.
    /// - GetRoot: O(1).
    /// 
    /// Memory Complexity:
    /// - O(N * L), where N is the number of words and L is the average length of the words.
    /// </summary>
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
        public bool IsEndOfWord { get; set; }
        public string? Word { get; set; }
    }

    public class Trie
    {
        private readonly TrieNode _root = new TrieNode();

        /// <summary>
        /// Inserts a word into the Trie.
        /// </summary>
        /// <param name="word">The word to insert.</param>
        /// <remarks>
        /// Time Complexity: O(L), where L is the length of the word.
        /// Memory Complexity: O(L) for each word inserted.
        /// </remarks>
        public void Insert(string word)
        {
            var node = _root;
            foreach (var ch in word)
            {
                // If the character is not already a child of the current node, add it
                if (!node.Children.ContainsKey(ch))
                {
                    node.Children[ch] = new TrieNode();
                }

                // Move to the child node
                node = node.Children[ch];
            }

            // Mark the end of the word and store the word
            node.IsEndOfWord = true;
            node.Word = word;
        }

        /// <summary>
        /// Gets the root node of the Trie.
        /// </summary>
        /// <returns>The root TrieNode.</returns>
        /// <remarks>
        /// Time Complexity: O(1).
        /// Memory Complexity: O(1).
        /// </remarks>
        public TrieNode GetRoot() => _root;
    }
}


