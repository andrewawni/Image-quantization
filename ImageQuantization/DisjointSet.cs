using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    public class DisjointSet<Type>
    {
        public class subset
        {
            public Type parent;
            public int rank=0;
            
            public subset(Type p)
            {
                parent = p;
            }
        }

        private List<subset> set=new List<subset>();
        public void MakeSet(Type x)
        {
            subset s=new subset(x);
            s.rank = 0;
            set.Add(s);
        }

        public Type Find(Type x)
        {
            subset s=new subset(x);
            if (!s.parent.Equals(x))
            {
                s.parent = Find(s.parent);
            }

            return s.parent;
        }

        public void Union(Type x, Type y)
        {
            Type xRoot = Find(x);
            Type yRoot = Find(y);
            
            subset xR=new subset(xRoot);
            subset yR=new subset(yRoot);
            
            
            if (xRoot.Equals(yRoot))
                return;
            
            if(xR.rank<yR.rank)
            {
                Type tmp = yRoot;
                yRoot = xRoot;
                xRoot = yRoot;
            }

            yR.parent = xRoot;
            if (xR.rank == yR.rank)
            {
                xR.rank++;
            }
        }

    }
}