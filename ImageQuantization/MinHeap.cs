using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class MinHeap
    {
        private int Size;
        private HeapNode [] Heap;
        private int [] indexer;

        public MinHeap(int Capacity)
        {
            Heap = new HeapNode[Capacity + 1]; // one based heap

            Heap[0] = new HeapNode(-1, long.MinValue); //unused index
            indexer = new int[Capacity + 1];
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
            HeapNode tmp = Heap[First];
            Heap[First] = Heap[Second];
            Heap[Second] = tmp;
        }

        private bool IsLeaf(int Index) // O(1)
        {
            return (Index > Size / 2 && Index <= Size);
        }

        private void Heapify(int Index) // O(log N)
        {
            if (IsLeaf(Index)) // Base Case (useless because we already check size in if conditions below).
                return;

            int _Left = Left(Index);
            int _Right = Right(Index);
            int Smallest;

            // position Index is larger than its childeren..
            // from presentation, slid 8, Lecture 9 (Binary Heap)
            if(_Left <= Size && Heap[Index].Distance > Heap[_Left].Distance)
                Smallest = _Left;
            else
                Smallest = Index;

            if(_Right <= Size && Heap[Smallest].Distance > Heap[_Right].Distance)
                Smallest = _Right;
            

            if(Smallest != Index)
            {
                indexer[Heap[Smallest].Id] = Index;
                indexer[Heap[Index].Id] = Smallest;
                SwapTwoNodes(Smallest, Index);
                Heapify(Smallest);
            }
        }
        public void MoveUp(int Current) // O(log n)
        {
            while (Heap[Current].Distance < Heap[Parent(Current)].Distance)
            {
                // Swap Indexes
                indexer[Heap[Current].Id] = Parent(Current);
                indexer[Heap[Parent(Current)].Id] = Current;
                // Swap Values
                SwapTwoNodes(Current, Parent(Current));
                // Move Up
                Current = Parent(Current);
            }
        }
        public void Insert(HeapNode NewNode)
        {
            Size++; // one based
            Heap[Size] = NewNode;
            indexer[NewNode.Id] = Size;
            // keep the structure of the binary heap..
            MoveUp(Size);
        }

        public HeapNode ExtractMin()
        {
            HeapNode Min = Heap[1];
            Heap[1] = Heap[Size];
            indexer[Heap[1].Id] = 1;
            Size--;
            Heapify(1);
            return Min;
        }

        public bool IsEmpty()
        {
            return (Size == 0);
        }

        public int HeapSize()
        {
            return Size;
        }

        public void UpdateHeap(int nodeId, long newDistance)
        {
            int index = indexer[nodeId];
            Heap[index].Distance = newDistance;
            MoveUp(index);
        }
    }
}
