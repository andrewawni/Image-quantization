using System;
using System.Collections;
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
    //Generic Weighted Graph Class implemented using an adjacency list
    public class GenericWeightedGraph<Type>
    {
        private List<KeyValuePair<UInt32, Type>>[] adjacencyList;
        
        private int numOfEdges=0;
        private int numOfVertices = 0;
        private List<Edge<Type>> edges;
        
        
        public GenericWeightedGraph(int size)
        {
            adjacencyList = new List<KeyValuePair<UInt32, Type>>[size];
            edges=new List<Edge<Type>>();
        }

        public void AddEdge(UInt32 source, UInt32 destination, Type weight)
        {
            if (adjacencyList[source] == null) adjacencyList[source] = new List<KeyValuePair<uint, Type>>();
            
            KeyValuePair<UInt32, Type> pair = new KeyValuePair<UInt32, Type>(destination, weight);
            adjacencyList[source].Add(pair);
            Edge<Type> e=new Edge<Type>(source,destination,weight);
            edges.Add(e);
            numOfEdges++;
        }
        

        //Sets the edge to the default value of the used type, i.e. removing it
        public void RemoveEdge(UInt32 source, UInt32 destination)
        {
            List<KeyValuePair<UInt32, Type>> list = adjacencyList[source];

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Key == destination)
                {
                    list.RemoveAt(i);
                    numOfEdges--;
                }
            }
        }

        public Type EdgeValue(UInt32 source, UInt32 destination)
        {
            List<KeyValuePair<UInt32, Type>> list = adjacencyList[source];
            
            foreach (var node in list)
                if (node.Key == destination) 
                    return node.Value;

            //Edge not set
            return default(Type);
        }

        public List<KeyValuePair<UInt32, Type>> AdjacentNodes(UInt32 index)
        {
            return adjacencyList[index];
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