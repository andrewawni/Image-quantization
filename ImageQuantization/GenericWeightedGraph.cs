using System;

namespace ImageQuantization
{
    //Generic Weighted Graph Class implemented using an adjacency matrix
    public class GenericWeightedGraph<Type>
    {
        private Type[,] adjacencyMatrix;
        
        public GenericWeightedGraph(int size)
        {
            adjacencyMatrix = new Type[size, size];
        }

        public void AddEdge(UInt32 source, UInt32 destination, Type weight)
        {
            adjacencyMatrix[source, destination] = weight;
        }

        //Sets the edge to the default value of the used type, i.e. removing it
        public void RemoveEdge(UInt32 source, UInt32 destination)
        {
            adjacencyMatrix[source, destination] = default(Type);
        }

        public Type EdgeValue(UInt32 source, UInt32 destination)
        {
            return adjacencyMatrix[source, destination];
        }
    }
}