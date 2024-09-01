using WordFinderLibrary.Interfaces;
using WordFinderLibrary.Strategies;

namespace WordFinderLibrary.Factories
{
    /// <summary>
    /// Factory class to create instances of search strategies.
    /// This factory is used to encapsulate the creation logic of different search strategies,
    /// allowing for easy extension and maintenance of the code.
    /// Benefits:
    /// - Decouples the client code from the concrete implementations of search strategies.
    /// - Simplifies the addition of new search strategies without modifying existing code.
    /// - Promotes the Open/Closed Principle by allowing new strategies to be added with minimal changes.
    /// </summary>
    public static class SearchStrategyFactory
    {
        /// <summary>
        /// Creates an instance of the specified search strategy type.
        /// </summary>
        /// <param name="strategyType">The type of search strategy to create.</param>
        /// <returns>An instance of the specified search strategy, or a default TrieSearchStrategy if creation fails.</returns>
        public static ISearchStrategy CreateStrategy(Type strategyType)
        {
            // Attempt to create an instance of the specified strategy type
            return Activator.CreateInstance(strategyType) as ISearchStrategy ?? new TrieSearchStrategy();
        }
    }
}
