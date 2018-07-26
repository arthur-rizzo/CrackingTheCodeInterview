using CrackingTheCodeInterview.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CrackingTheCodeInterview
{
    public class TreesAndGraphs
    {
		public static bool RouteBetweenNodes(GraphNode<int> n1, GraphNode<int> n2)
		{
			var result = false;

			//bidirectional search
			Queue<GraphNode<int>> q1 = new Queue<GraphNode<int>>();
			Queue<GraphNode<int>> q2 = new Queue<GraphNode<int>>();

			q1.Enqueue(n1);
			q2.Enqueue(n2);

			HashSet<GraphNode<int>> visitedFrom1 = new HashSet<GraphNode<int>>();
			HashSet<GraphNode<int>> visitedFrom2 = new HashSet<GraphNode<int>>();

			while (q1.Count > 0 || q2.Count > 0)
			{
				if(q1.Count > 0)
				{ 
					var current1 = q1.Dequeue();
					if (current1 == n2)
						return true;
					else if (visitedFrom2.Contains(current1))
						return true;

					visitedFrom1.Add(current1);
					if(current1.AdjacentNodes != null)
						foreach(var adjacentNode in current1.AdjacentNodes)
						{
							if (!visitedFrom1.Contains(adjacentNode))
								q1.Enqueue(adjacentNode);
						}
				}

				if (q2.Count > 0)
				{
					var current2 = q2.Dequeue();
					if (current2 == n1)
						return true;
					else if (visitedFrom1.Contains(current2))
						return true;

					visitedFrom2.Add(current2);
					if (current2.AdjacentNodes != null)
						foreach (var adjacentNode in current2.AdjacentNodes)
						{
							if (!visitedFrom2.Contains(adjacentNode))
								q2.Enqueue(adjacentNode);
						}
				}
			}

			return result;
		}

		public static TreeNode MinimalTree(int[] orderedValues)
		{
			if (orderedValues.Length == 1)
				return new TreeNode(orderedValues[0]);
			if(orderedValues.Length == 2)
			{
				return new TreeNode(orderedValues[1])
				{
					Left = new TreeNode(orderedValues[0])
				};
			}
			else
			{
				var middleIndex = orderedValues.Length / 2;

				TreeNode n = new TreeNode(orderedValues[middleIndex]);
				int[] leftArray = createLeftArray(orderedValues, middleIndex);
				int[] rightArray = createRightArray(orderedValues, middleIndex);

				n.Left = MinimalTree(leftArray);
				n.Right = MinimalTree(rightArray);

				return n;
			}
		}
		private static int[] createRightArray(int[] orderedValues, int middleIndex)
		{
			int rightArrayLength = orderedValues.Length - middleIndex - 1;
			int[] rightArray = new int[rightArrayLength];
			Array.Copy(orderedValues, middleIndex + 1, rightArray, 0, rightArrayLength);
			return rightArray;
		}
		private static int[] createLeftArray(int[] orderedValues, int middleIndex)
		{
			int leftArrayLength = middleIndex;
			int[] leftArray = new int[leftArrayLength];
			Array.Copy(orderedValues, leftArray, leftArrayLength);
			return leftArray;
		}
		
		public static SingleLinkedList<TreeNode>[] ListOfDepths(TreeNode node)
		{
			Dictionary<int, SingleLinkedList<TreeNode>> dictLists = new Dictionary<int, SingleLinkedList<TreeNode>>();
			_ListOfDepths(node, 0, dictLists);

			return dictLists.Values.ToArray();
		}
		private static void _ListOfDepths(TreeNode node, int index, Dictionary<int, SingleLinkedList<TreeNode>> listsDictionary)
		{
			SingleLinkedList<TreeNode> listForIndex;
			if (!listsDictionary.TryGetValue(index, out listForIndex))
				listsDictionary[index] = listForIndex = new SingleLinkedList<TreeNode>();

			var oldHead = listForIndex.Head;
			listForIndex.Head = new MyLinkedListNode<TreeNode>(node) { Next = oldHead };

			if (node.Left != null)
				_ListOfDepths(node.Left, index + 1, listsDictionary);

			if (node.Right != null)
				_ListOfDepths(node.Right, index + 1, listsDictionary);
		}

		public static bool IsBalanced(TreeNode node)
		{
			if (node == null)
				throw new ArgumentNullException(nameof(node));

			bool result;
			Depth(node, out result);

			return result;
		}
		private static int Depth(TreeNode n, out bool balanced)
		{
			balanced = true;
			if (n == null)
				return 0;

			var leftDepth = Depth(n.Left, out balanced);
			if (!balanced)
				return 0;

			var rigthDepth = Depth(n.Right, out balanced);
			if (!balanced)
				return 0;

			var diff = Math.Abs(rigthDepth - leftDepth);
			balanced = diff <= 1;

			return 1 + Math.Max(rigthDepth, leftDepth);
		}
	}
}
