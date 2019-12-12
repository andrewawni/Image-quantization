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

        //private List<subset> set=new List<subset>();
        private Dictionary<Type,subset> Dset=new Dictionary<Type, subset>();

        public void MakeSet(Type x)
        {
            subset s=new subset(x);
            s.rank = 0;
            //set.Add(s);
            Dset.Add(x,s);
        }

        public Type Find(Type x)
        {
            //subset s=new subset(x);
            
            if (!Dset[x].parent.Equals(x))
            {
                Dset[x].parent = Find(Dset[x].parent);
            }

            return Dset[x].parent;
        }

        public void Union(Type x, Type y)
        {
            Type xRoot = Find(x);
            Type yRoot = Find(y);
            
            //subset xR=new subset(xRoot);
            //subset yR=new subset(yRoot);
            
            
            if (xRoot.Equals(yRoot))
                return;
            
            if(Dset[x].rank<Dset[y].rank)
            {
                Type tmp = yRoot;
                yRoot = xRoot;
                xRoot = yRoot;
            }

            Dset[y].parent = xRoot;
            if (Dset[x].rank == Dset[y].rank)
            {
                Dset[x].rank++;
            }
        }

    }
}