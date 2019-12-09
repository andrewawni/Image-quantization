using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    class ImageGraph
    {
        private GenericWeightedGraph<Double> distanceBetweenColours;
        private HashSet<UInt32> uniquePixels;

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
            distanceBetweenColours = new GenericWeightedGraph<double>(uniquePixels.Count);
            SetGraphFromColours();
        }

        private HashSet<UInt32> GetColoursFromImage(Pixel[,] image)
        {
            uniquePixels = new HashSet<UInt32>();
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
                    Double eucledianDistance = CalculateEuclideanDistance(pixel1, pixel2);
                    distanceBetweenColours.AddEdge(pixel1, pixel2, eucledianDistance);
                }
            }
        }

        private Double CalculateEuclideanDistance(UInt32 p1Val, UInt32 p2Val)
        {
            Pixel p1 = Pixel.GetPixelFromDecimalValue(p1Val);
            Pixel p2 = Pixel.GetPixelFromDecimalValue(p2Val);
            Double rdiff = p1.getRed() - p2.getRed();
            Double gdiff = p1.getGreen() - p2.getGreen();
            Double bdiff = p1.getBlue() - p2.getBlue();

            return (rdiff * rdiff) + (gdiff * gdiff) + (bdiff * bdiff);
        }

        //Kept for debugging purposes
        public void Print()
        {
            foreach (var i in uniquePixels)
            {    
                var colour = "Colour " + i + " :";
                Console.Write(colour);
                foreach (var j in distanceBetweenColours.AdjacentNodes(i))
                    Console.Write(" " + j);
                Console.WriteLine();
            }
        }
    }
}