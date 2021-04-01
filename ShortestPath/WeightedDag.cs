namespace ShortestPath
{
	public class WeightedDag
	{
		public int ShortestPath(int nodes,int[,] paths)
		{
			var connections = new Graph(nodes,paths);
			var results = connections.ShortestPathFromStartToAllNodes();
			if (results[^1] == int.MaxValue)
				return -1;
			return results[^1];
		}
	}
}