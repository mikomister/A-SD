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

        public List<int[]> ParseTestsData(string filename)
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

        public void Prepare(string fileName, int n)
        {
            _tests = TestGeneratorN.GetTestCases(n);
            var sb = new StringBuilder();
            foreach (var c in _tests)
            {
                ArrayToSB(c, sb);
            }

            File.WriteAllText(fileName, sb.ToString());

        }

        private void ArrayToSB(IEnumerable<int> collection, StringBuilder sb)
        {
            var length = collection.Count();
            foreach (var t in collection)
            {
                sb.AppendFormat(" {0} ", t);
            }
            sb.Append("\n");
        }
    }
}