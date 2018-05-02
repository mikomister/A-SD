using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HeapSort
{
    public class TestGeneratorN
    {
        public static List<int[]> GetTestCases(int n, int from, int to)
        {
            var testCases = new List<int[]>();
            var rnd = new Random();
            for (var i = 0; i < n; i++)
            {
                testCases.Add(GenerateCollection(rnd, from, to));
            }

            return testCases;
        }

        private static int[] GenerateCollection(Random rnd, int from, int to)
        {
            var tRnd = new Random();
            return Enumerable
                .Repeat(100, rnd.Next(from, to))
                .Select(i => tRnd.Next(-100, 100))
                .ToArray();
        }
    }
}