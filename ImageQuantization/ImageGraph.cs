using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    class ImageGraph
    {
        private GenericWeightedGraph<Double> distanceBetweenColours;
        private HashSet<Pixel> uniquePixels;

        public ImageGraph()
        {
            distanceBetweenColours = new GenericWeightedGraph<double>(2 << 24);
        }

        public ImageGraph(Pixel[,] image)
        {
            distanceBetweenColours = new GenericWeightedGraph<double>(2 << 24);
            uniquePixels = GetColoursFromImage(image);
            SetGraphFromColours(uniquePixels);
        }

        public void SetImage(Pixel[,] image)
        {
            uniquePixels = GetColoursFromImage(image);
            SetGraphFromColours(uniquePixels);
        }
 
        private HashSet<Pixel> GetColoursFromImage(Pixel[,] image)
        {
            uniquePixels = new HashSet<Pixel>();
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    uniquePixels.Add(image[i, j]);
                }
            }
            return uniquePixels;
        }

        private void SetGraphFromColours(HashSet<Pixel> uniquePixels)
        {
            foreach (var pixel1 in uniquePixels)
            {
                foreach (var pixel2 in uniquePixels)
                {
                    Double eucledianDistance = calculateEuclideanDistance(pixel1,pixel2);
                    distanceBetweenColours.AddEdge(pixel1.getDecimalValue(), pixel2.getDecimalValue(), eucledianDistance);
                }
            }
        }

        private Double calculateEuclideanDistance(Pixel p1, Pixel p2)
        {
            Double rdiff = p1.getRed() - p2.getRed();
            Double gdiff = p1.getGreen() - p2.getGreen();
            Double bdiff = p1.getBlue() - p2.getBlue();
            
            return (rdiff * rdiff) + (gdiff * gdiff) + (bdiff * bdiff);
        }
    }
}
  