using CrackingTheCodeInterview;
using CrackingTheCodeInterview.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Test
{
	[TestClass]
    public class TreesAndGraphsTests
    {
		[TestMethod]
		[DataRow("1","", 1, 1, true)]
		[DataRow("1,2", "", 1, 2, false)]
		[DataRow("1,2", "1,2", 1, 2, true)]
		[DataRow("1,2,3", "1,2|2,3", 1, 3, true)]
		[DataRow("1,2,3,4", "1,2|2,3|3,4", 1, 4, true)]
		[DataRow("1,2,3,4", "1,2|3,4", 1, 4, false)]
		[DataRow("1,2,3,4,5,6", "1,2|1,3|1,4|1,5|5,6", 1, 6, true)]
		public void RouteBetweenNodes(string nodes, string edges, int startNode, int endNode, bool exptectedResult)
		{
			Graph g = new Graph();

			GraphNode<int> n1 = null;
			GraphNode<int> n2 = null;

			nodes.Split(',').Select(int.Parse).ToList().ForEach(x =>
			{
				var node = g.AddNode(x);

				if (x == startNode)
					n1 = node;
				if (x == endNode)
					n2 = node;
			});

			edges.Split('|').ToList().ForEach(x =>
			{
				if(x != "")
				{ 
					var y = x.Split(',');
					g.AddEdge(int.Parse(y[0]), int.Parse(y[1]));
				}
			});

			var actualResult = TreesAndGraphs.RouteBetweenNodes(n1, n2);
			Assert.AreEqual(exptectedResult, actualResult);

		}


		[TestMethod]
		public void MinimalTree()
		{
			int[] array = new int[] { 1,2,3,4,5,6,7,8,9,10,11,12 };

			var x = TreesAndGraphs.MinimalTree(array);
			Assert.Inconclusive();
		}

		[TestMethod]
		public void ListOfDepths()
		{
			int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			var x = TreesAndGraphs.MinimalTree(array);
			var result = TreesAndGraphs.ListOfDepths(x);
			Assert.Inconclusive();
		}

		[TestMethod]
		public void IsBalanced()
		{
			//uses minimal tree as a helper.. (good unit tests wouldn't depend on anoter)
			int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			var x = TreesAndGraphs.MinimalTree(array);
			var result = TreesAndGraphs.IsBalanced(x);
			Assert.IsTrue(result);

			TreeNode t = new TreeNode(1);
			t.Left = new TreeNode(2);
			t.Left.Left = new TreeNode(3);

			result = TreesAndGraphs.IsBalanced(t);
			Assert.IsFalse(result);

			t.Right = new TreeNode(4);
			result = TreesAndGraphs.IsBalanced(t);
			Assert.IsTrue(result);
		}
    }
}
