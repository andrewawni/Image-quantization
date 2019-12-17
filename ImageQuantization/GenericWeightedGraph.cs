using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    public class Edge<EType> 
    {
        public UInt32 source;
        public UInt32 dest;
        public EType weight;

        public Edge(UInt32 src, UInt32 des, EType wght)
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
        private List<Edge<Type>> edges;
        
        
        public GenericWeightedGraph(int size)
        {
            edges=new List<Edge<Type>>(size);
        }

        public GenericWeightedGraph()
        {
            edges = new List<Edge<Type>>();
        }
            

        public void AddEdge(UInt32 source, UInt32 destination, Type weight)
        {
            Edge<Type> e=new Edge<Type>(source,destination,weight);
            edges.Add(e);
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

        public List<Edge<Type>> GetEdges()
        {
            return edges;
        }
        
        // Debugging Only
        public void PrintEdges()
        {
            Console.WriteLine("Weight\t\t"+"Source\t\t"+"Dest");
            foreach (var e in edges)
            {
                Console.WriteLine(e.weight+"\t\t"+e.source+"\t\t"+e.dest);
            }
        }
    
    
    }
    
    
}