using WordFinderLibrary.Interfaces;
using WordFinderLibrary.Strategies;

namespace WordFinderLibrary.Factories
{
    public static class SearchStrategyFactory
    {
        public static ISearchStrategy CreateStrategy(Type strategyType)
        {
            return Activator.CreateInstance(strategyType) as ISearchStrategy ?? new TrieSearchStrategy();
        }
    }
}
