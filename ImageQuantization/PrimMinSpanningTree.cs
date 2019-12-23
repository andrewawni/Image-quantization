using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{ public class PrimMinSpanningTree
	{
		private int[] Parent;
		private long[] Distance;
		private bool[] Marked;
		private MinHeap PQ;
		HashSet<int> DistinctColors;

		public PrimMinSpanningTree(int Capacity, ImageGraph graph)
		{
			// Result Set
			Parent = new int[Capacity];
			Distance = new long[Capacity];

			// Check if in heap or not
			// 0 => in heap
			// 1 => not in heap
			Marked = new bool[Capacity];

			PQ = new MinHeap(Capacity);
			DistinctColors = graph.GetVertices();

			bool FirstNode = true;
			foreach (int Color in DistinctColors) // D log (D)
			{
				if (FirstNode == true)
				{
					PQ.Insert(new HeapNode(Color, 0));
					Distance[Color] = 0;
					FirstNode = false;
					Parent[Color] = -1;
				}
				else
				{
					PQ.Insert(new HeapNode(Color, long.MaxValue));
					Distance[Color] = long.MaxValue;
					Parent[Color] = -1;
				}
			}

			while (PQ.IsEmpty() == false)
			{
				HeapNode MinNode = PQ.ExtractMin();
				Marked[MinNode.Id] = true;

				foreach (var edge in graph.GetGenericGraph().GetEdges(MinNode.Id))
				{
					if (Marked[edge.dest] == false && Distance[edge.dest] > edge.weight)
					{
						PQ.UpdateHeap(edge.dest, edge.weight);
						Parent[edge.dest] = MinNode.Id;
						Distance[edge.dest] = edge.weight;
					}
				}
			}
		}

		public double SumOfTree()
		{
			double mstSum = 0;
			foreach (int Color in DistinctColors)
				mstSum += Math.Sqrt(Distance[Color]);

			return mstSum;
		}

		public void printMST()
		{
			long total_min_weight = 0;
			foreach (int Color in DistinctColors)
			{
				Console.WriteLine("Edge: " + Color + " - " + Parent[Color] +
				                  " weight: " + Distance[Color]);
				total_min_weight += Distance[Color];
			}

			Console.WriteLine("Total minimum key: " + total_min_weight);
		}

		public Dictionary<int,int> GenerateClusters(int num_clusters)
		{
			Dictionary<int, List<int>> adjacency_list = new Dictionary<int, List<int>>();
			Dictionary<int, int> mapped_pallete = new Dictionary<int, int>();
			
			//Removing the K-th most expensive edges: O(K*D)
			for (int i = 0; i < num_clusters - 1; i++)
			{
				long max_distance = 0;
				int extreme_color = -1;
				foreach (int color in DistinctColors)
				{
					if (Distance[color] > max_distance)
					{
						max_distance = Distance[color];
						extreme_color = color;
					}
				}

				Parent[extreme_color] = -1;
				Distance[extreme_color] = 0;
			}

			
			//converting the pallete into an adjacency list: O(D) 
			foreach (int color in DistinctColors)
			{
				if (!adjacency_list.ContainsKey(color))
					adjacency_list.Add(color, new List<int>());
				if (Parent[color] != -1 && !adjacency_list.ContainsKey(Parent[color]))
					adjacency_list.Add(Parent[color], new List<int>());

				if (Parent[color] != -1)
				{
					adjacency_list[Parent[color]].Add(color);
					adjacency_list[color].Add(Parent[color]);
				}
			}

			HashSet<int> vis = new HashSet<int>();
			Queue<int> q = new Queue<int>();
			
			//Assigning each color a new value after clustering: O(D)
			foreach (int color in DistinctColors)
			{
				if (!vis.Contains(color))
				{
					q.Enqueue(color);
					HashSet<int> cluster = new HashSet<int>();
					
					long totalR = 0, totalG = 0, totalB = 0;
					
					while (q.Count != 0)
					{
						int curr = q.First();
						cluster.Add(curr);
						q.Dequeue();
						vis.Add(curr);
						foreach (int child in adjacency_list[curr])
						{
							if (!vis.Contains(child))
								q.Enqueue(child);
						}
						Pixel curr_pixel = Pixel.GetPixelFromDecimalValue(curr);
						totalR += curr_pixel.getRed();
						totalG += curr_pixel.getGreen();
						totalB += curr_pixel.getBlue();

					}

					foreach (int subColor in cluster)
					{
						mapped_pallete[subColor] = (new Pixel((byte)(totalR/cluster.Count), (byte)(totalG/cluster.Count), (byte)(totalB/cluster.Count))).getDecimalValue();
					}
					
				}
			}
			return mapped_pallete;
		}
	}
}