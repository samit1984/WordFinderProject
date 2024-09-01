using System.Diagnostics;
using WordFinderLibrary;
using WordFinderLibrary.Factories;
using WordFinderLibrary.Strategies;

namespace PerformanceAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a 64x64 matrix filled with random characters
            var matrix = GenerateMatrix(64);
            // Get the list of words to find in the matrix
            var findWordsList = GetFindWordsList();
            // Get the list of search strategy types to test
            var strategyTypes = GetStrategyTypes();

            // Measure performance for each search strategy
            foreach (var strategyType in strategyTypes)
            {
                const int iterations = 10;
                MeasurePerformance(matrix, findWordsList, strategyType, iterations);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Measures the performance of a search strategy over a specified number of iterations.
        /// </summary>
        /// <param name="matrix">The matrix to search within.</param>
        /// <param name="words">The list of words to find.</param>
        /// <param name="strategyType">The type of search strategy to use.</param>
        /// <param name="iterations">The number of iterations to measure.</param>
        static void MeasurePerformance(IEnumerable<string> matrix, IEnumerable<string> words, Type strategyType, int iterations)
        {
            double totalMemoryUsed = 0;
            double totalTimeTaken = 0;

            for (int i = 0; i < iterations; i++)
            {
                // Force a garbage collection to clean up any unreferenced objects
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                // Measure initial memory usage
                var initialMemory = GC.GetTotalMemory(true);
                var stopwatch = Stopwatch.StartNew();

                // Create a WordFinder instance and set the search strategy
                var wordFinder = new WordFinder(matrix);
                var strategy = SearchStrategyFactory.CreateStrategy(strategyType);
                wordFinder.SetSearchStrategy(strategy);
                var foundWords = wordFinder.Find(words);

                // Stop the stopwatch and measure final memory usage
                stopwatch.Stop();
                var finalMemory = GC.GetTotalMemory(true);

                // Calculate memory used and time taken for this iteration
                double memoryUsed = finalMemory - initialMemory;
                double timeTaken = stopwatch.Elapsed.TotalMilliseconds;

                totalMemoryUsed += memoryUsed;
                totalTimeTaken += timeTaken;
            }

            // Calculate average memory used and time taken over all iterations
            double averageMemoryUsed = totalMemoryUsed / iterations;
            double averageTimeTaken = totalTimeTaken / iterations;
            Console.WriteLine($"Strategy: {strategyType.Name}, Average Time Taken: {Math.Round(averageTimeTaken, 2)} ms, Average Memory Used: {Math.Round(averageMemoryUsed / 1024.0, 2)} KB");
        }

        /// <summary>
        /// Generates a matrix of the specified size filled with random characters.
        /// </summary>
        /// <param name="size">The size of the matrix (size x size).</param>
        /// <returns>A list of strings representing the matrix.</returns>
        static List<string> GenerateMatrix(int size)
        {
            var matrix = new char[size, size];
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz";

            // Fill the matrix with random characters
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    matrix[i, j] = chars[random.Next(chars.Length)];
                }
            }

            // Insert found words into the matrix at random positions
            InsertWordsIntoMatrix(matrix, GetFoundWordsList());

            // Convert the matrix to a list of strings
            var matrixList = new List<string>();
            for (var i = 0; i < size; i++)
            {
                var row = new char[size];
                for (var j = 0; j < size; j++)
                {
                    row[j] = matrix[i, j];
                }
                matrixList.Add(new string(row));
            }

            return matrixList;
        }

        /// <summary>
        /// Inserts words into the matrix at random positions, either horizontally or vertically.
        /// </summary>
        /// <param name="matrix">The matrix to insert words into.</param>
        /// <param name="words">The list of words to insert.</param>
        static void InsertWordsIntoMatrix(char[,] matrix, List<string> words)
        {
            var random = new Random();
            foreach (var word in words)
            {
                // Randomly choose a starting position and direction (horizontal or vertical)
                var row = random.Next(matrix.GetLength(0) - word.Length);
                var col = random.Next(matrix.GetLength(1) - word.Length);
                var horizontal = random.Next(2) == 0;

                // Insert the word into the matrix
                for (var i = 0; i < word.Length; i++)
                {
                    if (horizontal)
                    {
                        matrix[row, col + i] = word[i];
                    }
                    else
                    {
                        matrix[row + i, col] = word[i];
                    }
                }
            }
        }

        /// <summary>
        /// Returns a list of words to find in the matrix, including found items, repeated items, and items not found in the matrix.
        /// </summary>
        /// <returns>A list of words to find.</returns>
        static List<string> GetFindWordsList()
        {
            return new List<string>
                {
                    // 100 found items
                    "algorithm", "binary", "compile", "debug", "execute",
                    "function", "hardware", "iterate", "java", "kernel",
                    "library", "memory", "network", "object", "program",
                    "search", "sort", "stack", "queue", "tree",
                    "graph", "hash", "heap", "linkedlist", "array",
                    "pointer", "recursion", "syntax", "variable", "loop",
                    "class", "method", "inheritance", "polymorphism", "encapsulation",
                    "abstraction", "interface", "exception", "thread", "process",
                    "concurrency", "parallelism", "synchronization", "deadlock", "livelock",
                    "starvation", "mutex", "semaphore", "monitor", "lock",
                    "racecondition", "atomicity", "consistency", "isolation", "durability",
                    "transaction", "rollback", "commit", "savepoint", "checkpoint",
                    "index", "primarykey", "foreignkey", "unique", "constraint",
                    "normalization", "denormalization", "sharding", "replication", "partitioning",
                    "backup", "restore", "snapshot", "log", "cache",
                    "buffer", "queue", "stack", "heap", "tree",
                    "graph", "trie", "bloomfilter", "hashtable", "hashmap",
                    "set", "list", "arraylist", "linkedlist", "deque",
                    "priorityqueue", "binarysearch", "linearsearch", "bubblesort", "quicksort",
                    "mergesort", "heapsort", "insertionsort", "selectionsort", "shellsort",
                    
                    // 50 repeated items randomly selected from the found items
                    "algorithm", "binary", "compile", "debug", "execute",
                    "function", "hardware", "iterate", "java", "kernel",
                    "library", "memory", "network", "object", "program",
                    "search", "sort", "stack", "queue", "tree",
                    "graph", "hash", "heap", "linkedlist", "array",
                    "pointer", "recursion", "syntax", "variable", "loop",
                    "class", "method", "inheritance", "polymorphism", "encapsulation",
                    "abstraction", "interface", "exception", "thread", "process",
                    
                    // 20 items not found in the matrix
                    "notfound1", "notfound2", "notfound3", "notfound4", "notfound5",
                    "notfound6", "notfound7", "notfound8", "notfound9", "notfound10",
                    "notfound11", "notfound12", "notfound13", "notfound14", "notfound15",
                    "notfound16", "notfound17", "notfound18", "notfound19", "notfound20"
                };
        }

        /// <summary>
        /// Returns a list of words that are inserted into the matrix.
        /// </summary>
        /// <returns>A list of words found in the matrix.</returns>
        static List<string> GetFoundWordsList()
        {
            return new List<string>
                {
                    "algorithm", "binary", "compile", "debug", "execute",
                    "function", "hardware", "iterate", "java", "kernel",
                    "library", "memory", "network", "object", "program",
                    "search", "sort", "stack", "queue", "tree",
                    "graph", "hash", "heap", "linkedlist", "array",
                    "pointer", "recursion", "syntax", "variable", "loop",
                    "class", "method", "inheritance", "polymorphism", "encapsulation",
                    "abstraction", "interface", "exception", "thread", "process",
                    "concurrency", "parallelism", "synchronization", "deadlock", "livelock",
                    "starvation", "mutex", "semaphore", "monitor", "lock",
                    "racecondition", "atomicity", "consistency", "isolation", "durability",
                    "transaction", "rollback", "commit", "savepoint", "checkpoint",
                    "index", "primarykey", "foreignkey", "unique", "constraint",
                    "normalization", "denormalization", "sharding", "replication", "partitioning",
                    "backup", "restore", "snapshot", "log", "cache",
                    "buffer", "queue", "stack", "heap", "tree",
                    "graph", "trie", "bloomfilter", "hashtable", "hashmap",
                    "set", "list", "arraylist", "linkedlist", "deque",
                    "priorityqueue", "binarysearch", "linearsearch", "bubblesort", "quicksort",
                    "mergesort", "heapsort", "insertionsort", "selectionsort", "shellsort"
                };
        }

        /// <summary>
        /// Returns a list of search strategy types to test.
        /// </summary>
        /// <returns>A list of search strategy types.</returns>
        static List<Type> GetStrategyTypes()
        {
            return new List<Type>
                {
                    typeof(BruteForceSearchStrategy),
                    typeof(DFSSearchStrategy),
                    typeof(TrieSearchStrategy)
                };
        }
    }
}
