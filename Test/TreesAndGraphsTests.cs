using CrackingTheCodeInterview;
using CrackingTheCodeInterview.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

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

		[TestMethod]
		public void IsBST()
		{
			var n = new TreeNode(2);
			n.Left = new TreeNode(0);
			n.Right = new TreeNode(3);
			n.Right.Right = new TreeNode(4);
			var result = TreesAndGraphs.IsBST(n);

			Assert.Inconclusive();
		}

		[TestMethod]
		public void Successor()
		{
			var n = new TreeNode(4)
			{
				Left = new TreeNode(2)
				{
					Left = new TreeNode(1),
					Right = new TreeNode(3)
				},
				Right = new TreeNode(6)
				{
					Left = new TreeNode(5),
					Right = new TreeNode(7)
				}
			};

			var result = TreesAndGraphs.Successor(n);
			Assert.AreEqual(result.Value, 5);

			result = TreesAndGraphs.Successor(n.Left.Right);
			Assert.AreEqual(result.Value, 4);

			result = TreesAndGraphs.Successor(n.Right.Right);
			Assert.AreEqual(result, null);

			result = TreesAndGraphs.Successor(n.Right);
			Assert.AreEqual(result.Value, 7);

			result = TreesAndGraphs.Successor(n.Left.Left);
			Assert.AreEqual(result.Value, 2);
		}

		[TestMethod]
		public void BuildOrder()
		{
			DirectedGraph g = new DirectedGraph();

			g.AddNode(1);
			g.AddNode(2);
			g.AddNode(3);
			g.AddNode(4);
			g.AddNode(5);
			g.AddNode(6);
			g.AddNode(7);
			g.AddNode(8);

			g.AddEdge(1, 2);
			g.AddEdge(2, 3);
			g.AddEdge(2, 4);
			g.AddEdge(3, 5);
			g.AddEdge(6, 7);
			g.AddEdge(7, 8);

			var result = TreesAndGraphs.BuildOrder(g);
			Assert.IsNotNull(result);
			foreach (var x in result)
				Trace.Write(x.Data.ToString() + "->");


			g = new DirectedGraph();
			g.AddNode(1);
			g.AddNode(2);
			g.AddNode(3);
			g.AddNode(4);

			g.AddEdge(1, 2);
			g.AddEdge(2, 3);
			g.AddEdge(4, 2);
			g.AddEdge(3, 4);

			result = TreesAndGraphs.BuildOrder(g);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void FistCommonAncestor()
		{
			TreeNode a = new TreeNode(1);
			TreeNode b = new TreeNode(1);
			TreeNode c = new TreeNode(1);
			TreeNode d = new TreeNode(1);
			TreeNode e = new TreeNode(1);

			a.Left = b;
			b.Left = c;
			b.Right = d;
			a.Right = e;

			var result = TreesAndGraphs.FirstCommonAncestor(a, a, a);
			Assert.AreSame(a, result);

			result = TreesAndGraphs.FirstCommonAncestor(a, a, b);
			Assert.AreSame(a, result);

			result = TreesAndGraphs.FirstCommonAncestor(a, b, c);
			Assert.AreSame(b, result);

			result = TreesAndGraphs.FirstCommonAncestor(a, a, c);
			Assert.AreSame(a, result);

			result = TreesAndGraphs.FirstCommonAncestor(a, d, e);
			Assert.AreSame(a, result);
		}

		[TestMethod]
		public void BSTSequence()
		{
			TreeNode n = new TreeNode(20)
			{
				Left = new TreeNode(10)
				{
					Left = new TreeNode(5)
				},
				Right = new TreeNode(30)
				{
					Left = new TreeNode(25),
					Right = new TreeNode(40)
				}
			};

			var result = TreesAndGraphs.BSTSequence(n);

			foreach(var r in result)
			{
				Trace.WriteLine("");
				foreach (var x in r)
					Trace.Write(x + ", ");
			}

		}

		[TestMethod]
		public void CheckSubtree()
		{
			TreeNode n = new TreeNode(4)
			{
				Left = new TreeNode(1)
				{
					Left = new TreeNode(4),
					Right = new TreeNode(11)
				},
				Right = new TreeNode(5)
				{
					Left = new TreeNode(444),
					Right = new TreeNode(115)
					{
						Right = new TreeNode(33)
						{
							Right = new TreeNode(22)
						}
					}
				}
			};

			var subtree = new TreeNode(5)
			{
				Left = new TreeNode(444),
				Right = new TreeNode(115)
				{
					Right = new TreeNode(33)
					{
						Right = new TreeNode(22)
					}
				}
			};

			var result = TreesAndGraphs.CheckSubtree(n, subtree);
			Assert.IsTrue(result);

			n = new TreeNode(4)
			{
				Left = new TreeNode(1)
				{
					Left = new TreeNode(4)
					{
						Left = new TreeNode(1)
					},
				}
			};

			subtree = new TreeNode(4)
			{
				Left = new TreeNode(1)
			};

			result = TreesAndGraphs.CheckSubtree(n, subtree);
			Assert.IsTrue(result);

			n.Left.Value = 2;
			result = TreesAndGraphs.CheckSubtree(n, subtree);
			Assert.IsTrue(result);

			n.Left.Left.Left.Value = 2;
			result = TreesAndGraphs.CheckSubtree(n, subtree);
			Assert.IsFalse(result);
		}


		[TestMethod]
		public void CurrentSum()
		{
			TreeNode n = new TreeNode(5)
			{
				Left = new TreeNode(-3)
				{
					Left = new TreeNode(5)
					{
						Left = new TreeNode(-3)
					}
				},
				Right = new TreeNode(8)
				{
					Left = new TreeNode(-2)
					{
						Left = new TreeNode(-9)
					},
					Right = new TreeNode(-11)
				}
			};

			var result = TreesAndGraphs.PathSum(n, 2);
			Assert.AreEqual(5, result);
		}
	}
}
