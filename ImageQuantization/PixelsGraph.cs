using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace ImageQuantization
{
    class PixelsGraph
    {
        private List<KeyValuePair<UInt32, Double>> [] AdjacencyList ; 

        public PixelsGraph()
        {
            AdjacencyList = new List<KeyValuePair<UInt32, Double>>[2 << 24];
        }

        public PixelsGraph(Pixel[,] image)
        {
            AdjacencyList = new List<KeyValuePair<UInt32, Double>>[2 << 24];
            HashSet<Pixel> UniquePixels = getColoursFromImage(image);
            setGraphFromColours(UniquePixels);
        }
        
        
        /*TODO:
         * -Make the class more abstract.
         * -Add: Add,Remove, Update functions.
         */

        private HashSet<Pixel> getColoursFromImage(Pixel[,] image)
        {
            HashSet<Pixel> uniquePixels = new HashSet<Pixel>();
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    uniquePixels.Add(image[i, j]);
                }
            }
            return uniquePixels;
        }

        private void setGraphFromColours(HashSet<Pixel> uniquePixels)
        {
            foreach (var pixel1 in uniquePixels)
            {
                foreach (var pixel2 in uniquePixels)
                {
                    Double EucledianDistance = calculateEucledianDistance(pixel1,pixel2);
                    KeyValuePair<UInt32, Double> P2ValueAndDistance = new KeyValuePair<uint, double>(pixel2.getDecimalValue(), EucledianDistance);
                    AdjacencyList[(int)pixel1.getDecimalValue()].Add(P2ValueAndDistance);
                }
            }
        }

        private Double calculateEucledianDistance(Pixel p1, Pixel p2)
        {
            Double Rdiff = p1.getRed() - p2.getRed();
            Double Gdiff = p1.getGreen() - p2.getGreen();
            Double Bdiff = p1.getBlue() - p2.getBlue();
            return Math.Sqrt(Math.Pow(Rdiff, 2) + Math.Pow(Gdiff, 2) + Math.Pow(Bdiff, 2));
        }
    }
}
  