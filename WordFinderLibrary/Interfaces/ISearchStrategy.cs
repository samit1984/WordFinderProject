namespace WordFinderLibrary.Interfaces
{
    /// <summary>
    /// Interface for search strategies used to find words in a character matrix.
    /// </summary>
    public interface ISearchStrategy
    {
        /// <summary>
        /// Finds the specified words in the given character matrix.
        /// </summary>
        /// <param name="matrix">The character matrix to search within.</param>
        /// <param name="words">The list of words to find in the matrix.</param>
        /// <returns>A dictionary where the keys are the found words and the values are their respective counts.</returns>
        Dictionary<string, int> FindWords(char[,] matrix, IList<string> words);
    }
}
