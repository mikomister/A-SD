using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Principal;

namespace HeapSort
{
    public class HeapSort
    {
        public int Iterations {  get; private set; }

        public HeapSort()
        {
            Iterations = 0;
        }
        
        private void Swap(IList<int> list, int a, int b)
        {
            int t = list[a];
            list[a] = list[b];
            list[b] = t;
        }
        
        private void Heapify(IList<int> list, int index, int heapSize)
        {
            var m = heapSize / 2;

            while (index < m)
            {
                int child = index * 2 + 1;
                if (child + 1 < heapSize && list[child + 1] > list[child])
                    child++;
                if (list[index] >= list[child]) break;
                this.Iterations++;
                Swap(list, index, child);
                index = child;
            }
        }

        private void BuildHeap(IList<int> list, int heapSize)
        {
            for (var i = list.Count / 2; i > -1; i--)
            {
                Heapify(list, i, heapSize);
            }
        }
        
        public void Sort(IList<int> list)
        {
            BuildHeap(list, list.Count);
            for (var i = list.Count - 1; i > 0; i--)
            {
                Swap(list, 0, i);
                BuildHeap(list, i);
            }
        }
        
        public void Sort(LinkedList<int> linkedList)
        {
            var list = LinkedListSortHelper(linkedList);
            BuildHeap(list, list.Count);
            for (var i = list.Count - 1; i > 0; i--)
            {
                Swap(list, 0, i);
                BuildHeap(list, i);
            }
        }

        private void Swap(IList<LinkedListNode<int>> list, int a, int b)
        {
            int t = list[a].Value;
            list[a].Value = list[b].Value;
            list[b].Value = t;
        }
        
        private void Heapify(IList<LinkedListNode<int>> list, int index, int heapSize)
        {
            var m = heapSize / 2;

            while (index < m)
            {
                int child = index * 2 + 1;
                if (child + 1 < heapSize && list[child + 1].Value > list[child].Value)
                    child++;
                if (list[index].Value >= list[child].Value) break;
                this.Iterations++;
                Swap(list, index, child);
                index = child;
            }
        }
        
        private void BuildHeap(IList<LinkedListNode<int>> list, int heapSize)
        {
            for (var i = list.Count / 2; i > -1; i--)
            {
                Heapify(list, i, heapSize);
            }
        }
        
        private List<LinkedListNode<int>> LinkedListSortHelper(LinkedList<int> linkedList)
        {
            var res = new List<LinkedListNode<int>>();
            var e = linkedList.First;
            while (e != null)
            {
                this.Iterations++;
                res.Add(e);
                e = e.Next;
            }

            return res;
        }
        
    }

}