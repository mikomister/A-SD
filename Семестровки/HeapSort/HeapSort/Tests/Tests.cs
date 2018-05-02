using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HeapSort.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test, TestCaseSource(typeof(TestsGenerator), nameof(TestsGenerator.TestCases))]
        public void IsEqualCollections(int[] received, int[] expected)
        {
            var heapSort = new HeapSort();
            heapSort.Sort(received);
            Array.Sort(expected);
            CollectionAssert.AreEqual(received, expected);
        }
    }

    public class TestsGenerator
    {
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                var rnd = new Random();
                for (var i = 0; i < 50; i++)
                {
                    var t1 = GenerateCollection(rnd);
                    var t2 = t1.ToArray();

                    yield return new TestCaseData(t1, t2);
                }
            }
        }

        private static int[] GenerateCollection(Random rnd)
        {
//            int size = rnd.Next(100, 10000);
            var tRnd = new Random();
//            int[] array = new int[size];
//            for (var i = 0; i < size; i++)
//                array[i] = tRnd.Next(-100, 100);
//            return array;
            return Enumerable
                .Repeat(100, rnd.Next(100, 10000))
                .Select(i => tRnd.Next(-1000, 1000))
                .ToArray();
        }
    }
}