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
            // one based heap
            Heap = new HeapNode[Capacity + 1];    //O(1)
            
            //unused index
            Heap[0] = new HeapNode(-1, long.MinValue);     //O(1)
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
            HeapNode tmp = Heap[First];    //O(1)
            Heap[First] = Heap[Second];    //O(1)
            Heap[Second] = tmp;            //O(1)
        }

        private bool IsLeaf(int Index) // O(1)
        {
            return (Index > Size / 2 && Index <= Size);    //O(1)
        }

        private void Heapify(int Index) // O(log n)
        {
            // Base Case (useless because we already check size in if conditions below).
            if (IsLeaf(Index)) //O(1)
                return;

            int _Left = Left(Index);      //O(1)
            int _Right = Right(Index);    //O(1)
            int Smallest;                 //O(1)

            // position Index is larger than its children..
            // from presentation, slid 8, Lecture 9 (Binary Heap)
            if(_Left <= Size && Heap[Index].Distance > Heap[_Left].Distance) //O(1)
                Smallest = _Left;                                            //O(1)
            else
                Smallest = Index;                                            //O(1)

            if(_Right <= Size && Heap[Smallest].Distance > Heap[_Right].Distance) //O(1)
                Smallest = _Right;
            

            if(Smallest != Index)
            {
                indexer[Heap[Smallest].Id] = Index;            //O(1)
                indexer[Heap[Index].Id] = Smallest;            //O(1)
                SwapTwoNodes(Smallest, Index);   //O(1)
                Heapify(Smallest);
            }
        }
        
        public void MoveUp(int Current) 
        {
            while (Heap[Current].Distance < Heap[Parent(Current)].Distance) // O(log n)
            {
                // Swap Indexes
                indexer[Heap[Current].Id] = Parent(Current);
                indexer[Heap[Parent(Current)].Id] = Current;
                // Swap Values
                SwapTwoNodes(Current, Parent(Current)); //O(1)
                // Move Up
                Current = Parent(Current);
            }
        }
        public void Insert(HeapNode NewNode) //O(log n)
        {
            Size++; // one based
            Heap[Size] = NewNode;
            indexer[NewNode.Id] = Size;
            // keep the structure of the binary heap..
            MoveUp(Size); //O(log n)
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
