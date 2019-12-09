using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    //Generic Weighted Graph Class implemented using an adjacency list
    public class GenericWeightedGraph<Type>
    {
        private List<KeyValuePair<UInt32, Type>>[] adjacencyList;
        
        public GenericWeightedGraph(int size)
        {
            adjacencyList = new List<KeyValuePair<UInt32, Type>>[size];
        }

        public void AddEdge(UInt32 source, UInt32 destination, Type weight)
        {
            KeyValuePair<UInt32, Type> pair = new KeyValuePair<UInt32, Type>(destination, weight);
            adjacencyList[source].Add(pair);
        }

        //Sets the edge to the default value of the used type, i.e. removing it
        public void RemoveEdge(UInt32 source, UInt32 destination)
        {
            List<KeyValuePair<UInt32, Type>> list = adjacencyList[source];

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Key == destination)
                {
                    list[i] = new KeyValuePair<UInt32, Type>(destination, default(Type));
                }
            }
        }

        public Type EdgeValue(UInt32 source, UInt32 destination)
        {
            List<KeyValuePair<UInt32, Type>> list = adjacencyList[source];
            
            foreach (var Node in list)
                if (Node.Key == destination) 
                    return Node.Value;

            //Edge not set
            return default(Type);
        }
    }
}