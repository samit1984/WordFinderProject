namespace WordFinderLibrary.Interfaces
{
    public interface ISearchStrategy
    {
        Dictionary<string, int> FindWords(char[,] matrix, IList<string> words);
    }
}
