using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class MinHeap
    {
        private int Size;
        private long [] Heap;
        public MinHeap(int Capacity)
        {
            Heap = new long[Capacity + 1]; // one based heap
            Size = 0;
        }

        private int Parent(int Index) // O(1)
        {
            return Index / 2;
        }

        private int Left(int Index) // O(1)
        {
            return Index * 2;
        }

        private int Right(int Index) // O(1)
        {
            return Index * 2 + 1;
        }

        private void SwapTwoNodes(int First, int Second) // O(1)
        {
            long tmp = Heap[First];
            Heap[First] = Heap[Second];
            Heap[Second] = tmp;
        }

        private bool IsLeaf(int Index) // O(1)
        {
            return (Index >= Size / 2 && Index <= Size);
        }

        private void Heapify(int Index) // O(log N)
        {
            if (IsLeaf(Index)) // Base Case (useless because we already check size in if conditions below).
            {
                return;
            }

            int _Left = Left(Index);
            int _Right = Right(Index);
            int Smallest;

            // position Index is larger than its childeren..
            // from presentation, slid 8, Lecture 9 (Binary Heap)
            if(_Left <= Size && Heap[Index] > Heap[_Left])
            {
                Smallest = _Left;
            }
            else
            {
                Smallest = Index;
            }

            if(_Right <= Size && Heap[Smallest] > Heap[_Right])
            {
                Smallest = _Right;
            }

            if(Smallest != Index)
            {
                SwapTwoNodes(Smallest, Index);
                Heapify(Smallest);
            }
        }

        public void Insert(long Element)
        {
            Size++; // one based
            Heap[Size] = Element;

            // keep the structure of the binary heap..
            int Current = Size;
            while (Heap[Current] < Heap[Parent(Current)])
            {
                SwapTwoNodes(Current, Parent(Current));
                Current = Parent(Current);
            }
        }

        public long ExtractMin()
        {
            long Min = Heap[1];
            Heap[1] = Heap[Size];
            Size--;
            Heapify(1);
            return Min;
        }
    }
}
