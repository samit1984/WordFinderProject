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
            var matrix = GenerateMatrix(64);
            var findWordsList = GetFindWordsList();
            var strategyTypes = GetStrategyTypes();

            foreach (var strategyType in strategyTypes)
            {
                const int iterations = 10;
                MeasurePerformance(matrix, findWordsList, strategyType, iterations);
            }
            Console.ReadKey();
        }

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

                var initialMemory = GC.GetTotalMemory(true);
                var stopwatch = Stopwatch.StartNew();

                var wordFinder = new WordFinder(matrix);
                var strategy = SearchStrategyFactory.CreateStrategy(strategyType);
                wordFinder.SetSearchStrategy(strategy);
                var foundWords = wordFinder.Find(words);

                stopwatch.Stop();
                var finalMemory = GC.GetTotalMemory(true);

                double memoryUsed = finalMemory - initialMemory;
                double timeTaken = stopwatch.Elapsed.TotalMilliseconds;

                totalMemoryUsed += memoryUsed;
                totalTimeTaken += timeTaken;
            }

            double averageMemoryUsed = totalMemoryUsed / iterations;
            double averageTimeTaken = totalTimeTaken / iterations;
            Console.WriteLine($"Strategy: {strategyType.Name}, Average Time Taken: {Math.Round(averageTimeTaken, 2)} ms, Average Memory Used: {Math.Round(averageMemoryUsed / 1024.0, 2)} KB");
            
        }

        static List<string> GenerateMatrix(int size)
        {
            var matrix = new char[size, size];
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz";

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    matrix[i, j] = chars[random.Next(chars.Length)];
                }
            }

            // Insert found words into the matrix at random positions
            InsertWordsIntoMatrix(matrix, GetFoundWordsList());

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

        static void InsertWordsIntoMatrix(char[,] matrix, List<string> words)
        {
            var random = new Random();
            foreach (var word in words)
            {
                var row = random.Next(matrix.GetLength(0) - word.Length);
                var col = random.Next(matrix.GetLength(1) - word.Length);
                var horizontal = random.Next(2) == 0;

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
