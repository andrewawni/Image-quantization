using System;
using System.Collections.Generic;
using System.Collections;

namespace ImageQuantization
{
    public class ToCompare<CType> : IComparer<Edge<CType>>
    {
        public int Compare(Edge<CType> x, Edge<CType> y)
        {
            if (x.weight.Equals(y.weight) || y.weight.Equals(x.weight))
            {
                return 0;
            }

            return ((new CaseInsensitiveComparer().Compare(x.weight, y.weight)));
        }
    }
    public class MinumumSpanningTree
    {
        private DisjointSet<UInt32> set=new DisjointSet<UInt32>();
        public GenericWeightedGraph<double> tree=new GenericWeightedGraph<double>(2<<24);

        public void ConstructTree(ImageGraph graph)
        {
            foreach (var v in graph.GetVertices())
            {
                set.MakeSet(v);
            }
            
            graph.GetGenericGraph().GetEdges().Sort(new ToCompare<double>());
            
            foreach (var e in graph.GetGenericGraph().GetEdges())
            {
                if (set.Find(e.source) != set.Find(e.dest))
                {
                    tree.AddEdge(e.source,e.dest,e.weight);
                    set.Union(set.Find(e.source),set.Find(e.dest));
                }
            }
        }
        
        public double SumOfTree()
        {
            double mstSum = 0;
            foreach (var edge in tree.GetEdges())
            {
                mstSum += Math.Sqrt(edge.weight);
            }

            return mstSum;
        }
    }
}