using System;
using System.Collections.Generic;
using System.Text;
namespace CrackingTheCodeInterview.Structures
{
    public class DirectedGraph
    {
		private Dictionary<int, GraphNode<int>> nodes = new Dictionary<int, GraphNode<int>>();

		public GraphNode<int> AddNode(int value)
		{
			var x = new GraphNode<int>() { Data = value };
			nodes.Add(value, x);

			return x;
		}
		
		public void AddEdge(int a, int b)
		{
			var aNode = nodes[a];
			var bNode = nodes[b];

			aNode.AdjacentNodes.Add(bNode);
		}

		public IEnumerable<GraphNode<int>> Nodes
		{
			get
			{
				return nodes?.Values;
			}
		}
	}
}
