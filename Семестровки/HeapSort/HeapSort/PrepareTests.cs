using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace HeapSort
{
    public class PrepareTests
    {
        private List<int []> _tests;

        public List<int[]> ParseTestsToCollection(string filename)
        {
            var text =  File.ReadAllText(filename).Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var res = new List<int[]>();
            for (var i=0; i< text.Length; i++)
            {
                var tArray = Array.ConvertAll(text[i].Split(" ", StringSplitOptions.RemoveEmptyEntries), int.Parse);
                res.Add(tArray);
            }
            return res;

        }
        
        public List<LinkedList<int>> ParseTestsToLinkedCollection(string filename)
        {
            var text =  File.ReadAllText(filename).Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var res = new List<LinkedList<int>>();
            for (var i=0; i< text.Length; i++)
            {
                var tArray = Array.ConvertAll(text[i].Split(" ", StringSplitOptions.RemoveEmptyEntries), int.Parse);
                res.Add(new LinkedList<int>(tArray));
            }
            return res;

        }

        public void Prepare(string fileName, int n, int from, int to)
        {
            _tests = TestGeneratorN.GetTestCases(n, from, to);
            var sb = new StringBuilder();
            foreach (var c in _tests)
            {
                ArrayToSB(c, sb);
            }

            File.WriteAllText(fileName, sb.ToString());

        }

        private void ArrayToSB(IEnumerable<int> collection, StringBuilder sb)
        {
            foreach (var t in collection)
            {
                sb.AppendFormat(" {0} ", t);
            }
            sb.Append("\n");
        }
    }
}