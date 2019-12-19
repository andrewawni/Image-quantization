using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class PrimMinSpanningTree
    {
        private int [] Parent;
        private long [] Distance;
        private bool [] Marked;
        private MinHeap PQ;
        HashSet<int> DistinctColors;
        public PrimMinSpanningTree(int Capacity, ImageGraph graph)
        {
            // Result Set
            Parent = new int[Capacity];
            Distance = new long[Capacity];

            // Check if in heap or not
            // 0 => in heap
            // 1 => not in heap
            Marked = new bool[Capacity];

            PQ = new MinHeap(Capacity);
            DistinctColors = graph.GetVertices();

            bool FirstNode = true;
            foreach (int Color in DistinctColors) // D log (D)
            {
                if(FirstNode == true)
                {
                    PQ.Insert(new HeapNode(Color, 0));
                    Distance[Color] = 0;
                    FirstNode = false;
                }
                else
                {
                    PQ.Insert(new HeapNode(Color, long.MaxValue));
                    Distance[Color] = long.MaxValue;
                    Parent[Color] = -1;  
                }
            }

            while(PQ.IsEmpty() == false)
            {
                HeapNode MinNode = PQ.ExtractMin();
                Marked[MinNode.Id] = true;

                foreach(var edge in graph.GetGenericGraph().GetEdges(MinNode.Id))
                {
                    if(Marked[edge.dest] == false && Distance[edge.dest] > edge.weight)
                    {
                        PQ.UpdateHeap(edge.dest, edge.weight);
                        Parent[edge.dest] = MinNode.Id;
                        Distance[edge.dest] = edge.weight;
                    }
                }
            }
        }
        public double SumOfTree()
        {
            double mstSum = 0;
            foreach (int Color in DistinctColors)
                mstSum += Math.Sqrt(Distance[Color]);

            return mstSum;
        }

        public void printMST()
        {
            long total_min_weight = 0;
            foreach (int Color in DistinctColors)
            {
                Console.WriteLine("Edge: " + Color + " - " + Parent[Color] +
                        " weight: " + Distance[Color]);
                total_min_weight += Distance[Color];
            }
            Console.WriteLine("Total minimum key: " + total_min_weight);
        }
    }
}
