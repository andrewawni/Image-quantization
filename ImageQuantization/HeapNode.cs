using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    public class HeapNode
    {
        public int Id;
        public long Distance;
        public HeapNode(int Id, long Distance)
        {
            this.Id = Id;
            this.Distance = Distance;
        }
    }
}
