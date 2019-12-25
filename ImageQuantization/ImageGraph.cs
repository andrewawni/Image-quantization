using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    public class ImageGraph
    {
        private HashSet<int> uniquePixels;

        public ImageGraph(Pixel[,] image)
        {
            uniquePixels = GetColoursFromImage(image);   //O(N×M)
        }

        private HashSet<int> GetColoursFromImage(Pixel[,] image)
        {
            uniquePixels = new HashSet<int>();            //O(1)
            int lengthI = image.GetLength(0);  //O(1)
            int lengthJ = image.GetLength(1);  //O(1)
            for (int i = 0; i < lengthI; i++)             //O(N)
            {
                for (int j = 0; j < lengthJ; j++)         //O(M) 
                    uniquePixels.Add(image[i, j].getDecimalValue()); //O(1)
            }
            return uniquePixels;                         //O(1)
        }


        public HashSet<int> GetVertices()
        {
            return uniquePixels;                        //O(1)
        }
    }
}