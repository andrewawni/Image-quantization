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
        private DisjointSet<int> set=new DisjointSet<int>();
        public GenericWeightedGraph<double> tree=new GenericWeightedGraph<double>(2<<24);

        public void ConstructTree(ImageGraph graph)
        {
            for (int i = 0; i < graph.GetGenericGraph().NumberOfVertices(); ++i)
            {
                set.MakeSet(i);
            }
            
            graph.GetGenericGraph().GetEdges().Sort(new ToCompare<double>());

            foreach (var e in graph.GetGenericGraph().GetEdges())
            {
                if (set.Find(e.source) != set.Find(e.dest))
                {
                    tree.AddEdge((UInt32)e.source,(UInt32)e.dest,e.weight);
                    set.Union(set.Find(e.source),set.Find(e.dest));
                }
            }
        }
    }
}