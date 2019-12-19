using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    public class Edge 
    {
        public int source;
        public int dest;
        public long weight;

        public Edge(int src, int des, long wght)
        {
            source = src;
            dest = des;
            weight = wght;
        }
    }
    
    public class GenericWeightedGraph<Type>
    {
        private int numOfEdges=0;
        private int numOfVertices = 0;
        private List<Edge> [] edges;
        
        
        public GenericWeightedGraph(int size)
        {
            edges=new List<Edge>[size];
        }

        public GenericWeightedGraph()
        {
            edges = new List<Edge>[1 << 24];
        }
            

        public void AddEdge(int source, int destination, long weight)
        {
            Edge e = new Edge(source, destination, weight);
            if(edges[source] == null)
            {
                edges[source] = new List<Edge>();
            }
            edges[source].Add(e);
            numOfEdges++;
        }
        public int NumberOfEdges()
        {
            return numOfEdges;
        }

        public void IncrementVertices()
        {
            numOfVertices++;
        }

        public int NumberOfVertices()
        {
            return numOfVertices;
        }

        public List<Edge> GetEdges(int index)
        {
            return edges[index];
        }
    }
}