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
            var file = "Tests.txt";
            var prepareTests = new PrepareTests();
            
//            prepareTests.Prepare(file, 100);
            
            var tests = prepareTests.ParseTestsData(file);
            RunTests(tests, "TestResults.txt");
            
            void RunTests(List<int[]> collection, string filename)
            {
                Stopwatch timer = new Stopwatch();
                var TestResults = new StringBuilder();

                foreach (var t in collection)
                {
                    var heapSort = new HeapSort();
                    timer = Stopwatch.StartNew();
                    heapSort.Sort(t);
                    timer.Stop();
                    TestResults.AppendFormat("{0}; {1}; {2}\n" , t.Length, 
                        (double)timer.ElapsedMilliseconds,
                        heapSort.Iterations);
                }
                File.WriteAllText(filename, TestResults.ToString());
            }
            
        }
    }
}