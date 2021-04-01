namespace ShortestPath
{
	public class ConnectedNodes
	{
		public ConnectedNodes(int connectedNode, int weight)
		{
			ConnectedNode = connectedNode;
			Weight = weight;
		}

		public int ConnectedNode { get; set; }
		public int Weight { get; set; }
	}
}