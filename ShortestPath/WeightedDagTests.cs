using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ShortestPath
{
	public class WeightedDagTests
	{
		[Test]
		public void Nothing()
		{
			var map = new WeightedDag();
			var result = map.ShortestPath(4,new[,]
			{
				{ 0, 1, 1 }, { 0, 2, 5 }, { 0, 3, 5 }, { 1, 3, 3 },
				{ 2, 3, 1 }
			});
			Assert.That(result,Is.EqualTo(4));
		}
	}

	public class WeightedDag
	{
		public int ShortestPath(int nodes,int[,] paths)
		{
			var connections = MakeConnections(paths);
			return connections.Sum(x => x.Value);
		}

		private Dictionary<int[],int> MakeConnections(int[,] paths)
		{
			var shortestPath = int.MaxValue;
			var connections = new Dictionary<int[], int>();
			for(var row=0;row<paths.GetLength(0);row++)
			{
				var distance = paths[row, 2];
				var value = paths[row, 0];
				var value2 = paths[row, 1];
				var nextNode = 0;
				var keys = (connections.Keys.Where(x => x.Contains(value) || x.Contains(value2))).ToList();
				if (keys.Count>0 && keys[0][0]==value)
				{
					if (shortestPath > distance)
					{
						connections.Remove(keys[0]);
						shortestPath = distance;
						connections[new int[] {value,value2}] = shortestPath;
					}
				}
				if (keys.Count > 0 && keys[0][0] != value)
				{
					nextNode = value;
				}
				else
				{
					shortestPath = distance;
					connections[new[] {value,value2}] = distance;
					nextNode = value2;
				}
			}
			return connections;
		}
	}
}