using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    public class ImageGraph
    {
        private HashSet<int> uniquePixels;

        public ImageGraph()
        {
        }

        public ImageGraph(Pixel[,] image)
        {
            uniquePixels = GetColoursFromImage(image);
        }

        public void SetImage(Pixel[,] image)
        {
            uniquePixels = GetColoursFromImage(image);
        }

        private HashSet<int> GetColoursFromImage(Pixel[,] image)
        {
            //TODO: Set lengths to variables.
            //TODO: Maybe use an array 
            uniquePixels = new HashSet<int>();
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    uniquePixels.Add(image[i, j].getDecimalValue());
                }
            }

            return uniquePixels;
        }


        public HashSet<int> GetVertices()
        {
            return uniquePixels;
        }
    }
}