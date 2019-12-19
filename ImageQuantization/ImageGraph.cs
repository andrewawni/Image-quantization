using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    public class ImageGraph
    {
        private GenericWeightedGraph<double> distanceBetweenColours;
        private HashSet<int> uniquePixels;

        public ImageGraph()
        {
            distanceBetweenColours = new GenericWeightedGraph<double>(2 << 24);
        }

        public ImageGraph(Pixel[,] image)
        {
            uniquePixels = GetColoursFromImage(image);
            distanceBetweenColours = new GenericWeightedGraph<double>(2 << 24);
            SetGraphFromColours();
        }

        public void SetImage(Pixel[,] image)
        {
            uniquePixels = GetColoursFromImage(image);
            distanceBetweenColours = new GenericWeightedGraph<double>(2 << 24);
            SetGraphFromColours();
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

        private void SetGraphFromColours()
        {
            foreach (var pixel1 in uniquePixels)
            {
                foreach (var pixel2 in uniquePixels)
                {
                    long eucledianDistance = CalculateEuclideanDistance(pixel1, pixel2);
                    distanceBetweenColours.AddEdge(pixel1, pixel2, eucledianDistance);
                }
                distanceBetweenColours.IncrementVertices();
            }
        }

        private long CalculateEuclideanDistance(int p1Val, int p2Val)
        {
            Pixel p1 = Pixel.GetPixelFromDecimalValue(p1Val);
            Pixel p2 = Pixel.GetPixelFromDecimalValue(p2Val);
            long rdiff = p1.getRed() - p2.getRed();
            long gdiff = p1.getGreen() - p2.getGreen();
            long bdiff = p1.getBlue() - p2.getBlue();

            return (rdiff * rdiff) + (gdiff * gdiff) + (bdiff * bdiff);
        }

        public GenericWeightedGraph<Double> GetGenericGraph()
        {
            return distanceBetweenColours;
        }

        public HashSet<int> GetVertices()
        {
            return uniquePixels;
        }
    }
}