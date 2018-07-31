using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview.Structures
{
    public class GraphNode<T>
    {
		public GraphNode()
		{

		}

		public GraphNode(T data)
		{
			Data = data;
		}

		public T Data { get; set; }

		public List<GraphNode<T>> AdjacentNodes { get; set; } = new List<GraphNode<T>>();
    }
}
