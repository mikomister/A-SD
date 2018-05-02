using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework.Internal;

namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var testsRunner = new TestsRunner();
            var file = "Tests.txt";
            var prepareTests = new PrepareTests();

//            prepareTests.Prepare(file, 100, 100, 10000);
//            prepareTests.Prepare(file, 5, 10, 15);

            var testCollections = prepareTests.ParseTestsToCollection(file);
            testsRunner.Run(testCollections, "CollectionResults.txt");

            var testLinkedCollections = prepareTests.ParseTestsToLinkedCollection(file);
            testsRunner.Run(testLinkedCollections, "LinkedCollectionResults.txt");

//            showCollection(testLinkedCollections[0]);
//            Console.WriteLine("The Same Collection sorted as List");
//            showCollection(testCollections[0]);

            void showCollection(IEnumerable<int> collection)
            {
                foreach (var e in collection)
                {
                    Console.WriteLine(" {0} ", e);
                }

                Console.WriteLine("\n");
            }
        }

        public class TestsRunner
        {
            public void Run(List<int[]> collection, string filename)
            {
                Stopwatch timer = new Stopwatch();
                var TestResults = new StringBuilder();

                foreach (var t in collection)
                {
                    var heapSort = new HeapSort();
                    timer = Stopwatch.StartNew();
                    heapSort.Sort(t);
                    timer.Stop();
                    TestResults.AppendFormat("{0}; {1}; {2}\n", t.Length,
                        (double) timer.ElapsedMilliseconds,
                        heapSort.Iterations);
                }

                File.WriteAllText(filename, TestResults.ToString());
            }

            public void Run(List<LinkedList<int>> collection, string filename)
            {
                Stopwatch timer = new Stopwatch();
                var TestResults = new StringBuilder();

                foreach (var t in collection)
                {
                    var heapSort = new HeapSort();
                    timer = Stopwatch.StartNew();
                    heapSort.Sort(t);
                    timer.Stop();
                    TestResults.AppendFormat("{0}; {1}; {2}\n", t.Count,
                        (double) timer.ElapsedMilliseconds,
                        heapSort.Iterations);
                }

                File.WriteAllText(filename, TestResults.ToString());
            }
        }
    }
}