using System.Collections.Generic;
using System.Linq;

namespace ShortestPath
{
	/// <summary>
	/// Initially started with:
	/// https://medium.com/outco/how-to-build-a-graph-data-structure-d779d822f9b4
	/// and ended up with: 
	/// https://www.geeksforgeeks.org/topological-sorting/
	/// </summary>
	public class Graph
	{
		public Graph(int nodes, int[,] paths)
		{
			this.nodes = nodes;
			connectedNodes = new List<ConnectedNodes>[this.nodes];
			for (var i = 0; i < this.nodes; ++i)
				connectedNodes[i] = new List<ConnectedNodes>();
			for (var index = 0; index < paths.GetLength(0); index++)
				AddEdge(paths[index, 0], paths[index, 1], paths[index, 2]);
		}

		private readonly int nodes;
		private readonly List<ConnectedNodes>[] connectedNodes;

		private void AddEdge(int startNode, int endNode, int weight)
		{
			var node = new ConnectedNodes(endNode, weight);
			connectedNodes[startNode].Add(node);
		}

		public int[] ShortestPathFromStartToAllNodes()
		{
			var stack = new Stack<int>();
			var distances = new int[nodes];
			var visited = new bool[nodes];
			InitializeVisitedNodes(visited);
			LinkNodes(visited, stack);
			InitializeDistances(distances);
			distances[0] = 0;
			while (stack.Count != 0)
				FillDistances(stack, distances);
			return distances;
		}

		private void FillDistances(Stack<int> stack, int[] distance)
		{
			var start = stack.Pop();
			if (distance[start] != int.MaxValue)
				foreach (var currentNode in connectedNodes[start].Where(currentNode
					=> distance[currentNode.ConnectedNode] > distance[start] + currentNode.Weight))
					distance[currentNode.ConnectedNode] = distance[start] + currentNode.Weight;
		}

		private void InitializeDistances(int[] distance)
		{
			for (var i = 0; i < nodes; i++)
				distance[i] = int.MaxValue;
		}

		private void InitializeVisitedNodes(bool[] visited)
		{
			for (var i = 0; i < nodes; i++)
				visited[i] = false;
		}

		private void LinkNodes(bool[] visited, Stack<int> stack)
		{
			for (var i = 0; i < nodes; i++)
				if (visited[i] == false)
					OrderNodes(i, visited, stack);
		}

		private void OrderNodes(int node, bool[] visited, Stack<int> stack)
		{
			visited[node] = true;
			foreach (var currentNode in connectedNodes[node].
				Where(currentNode => !visited[currentNode.ConnectedNode]))
				OrderNodes(currentNode.ConnectedNode, visited, stack);
			stack.Push(node);
		}
	}
}