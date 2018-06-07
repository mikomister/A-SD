using System;
using System.Collections.Generic;

namespace AVLTree
{
    class Program
    {
        static void Main(string[] args)
        {

            var tree = new AvlTree<int>();
            tree.Insert(new int[] {1, 2, 3, 4, 6, 7, 8, 11, 0, 5, 23, 40, 9, 12});
            tree.Remove(5);
            tree.Head.Print(2, 10);
            Console.WriteLine();
            foreach (var item in tree)
            {
                Console.WriteLine(item);
            }
        }
    }
}