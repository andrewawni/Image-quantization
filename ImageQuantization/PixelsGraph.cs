using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class PixelsGraph
    {
        private Dictionary<UInt32, List<Pixel>> AdjacencyList;
        
        public PixelsGraph()
        {
            AdjacencyList = new Dictionary<UInt32, List<Pixel>>();
        }
    }
}
  