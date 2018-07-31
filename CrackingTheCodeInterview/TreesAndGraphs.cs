using CrackingTheCodeInterview.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

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
				if (q1.Count > 0)
				{
					var current1 = q1.Dequeue();
					if (current1 == n2)
						return true;
					else if (visitedFrom2.Contains(current1))
						return true;

					visitedFrom1.Add(current1);
					if (current1.AdjacentNodes != null)
						foreach (var adjacentNode in current1.AdjacentNodes)
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
			if (orderedValues.Length == 2)
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

		public static bool IsBST(TreeNode n)
		{
			int min, max;
			var result = isBST(n, out min, out max);

			return result;
		}

		private static bool isBST(TreeNode n, out int minSubtreeValue, out int maxSubtreeValue)
		{
			minSubtreeValue = maxSubtreeValue = n.Value;

			bool result = true;
			if (n.Left != null)
			{
				int leftSubtreeMin, leftSubtreeMax;
				var leftResult = isBST(n.Left, out leftSubtreeMin, out leftSubtreeMax);

				if (!leftResult)
					return false; //means leftSubtree is not a BST.. so root tree cant be.

				if (leftSubtreeMax >= n.Value)
					return false; //found node on the left greater than N.value

				minSubtreeValue = leftSubtreeMin;
			}

			if (n.Right != null)
			{
				int rightSubtreeMin, rightSubtreeMax;
				var leftResult = isBST(n.Right, out rightSubtreeMin, out rightSubtreeMax);

				if (!leftResult)
					return false; //means leftSubtree is not a BST.. so root tree cant be.

				if (rightSubtreeMin <= n.Value)
					return false; //found node on the right subtree less than N.value

				maxSubtreeValue = rightSubtreeMax;
			}

			return result;
		}

		public static TreeNode Successor(TreeNode n)
		{
			if (n == null)
				return null;

			if (n.Right != null)
			{
				var leftMost = n.Right;
				while (leftMost.Left != null)
					leftMost = leftMost.Left;

				return leftMost;
			}
			else
			{
				//go up until we find a node which is left child of its parent.
				//the parent is our answer

				var current = n;
				var parent = n.Parent;

				while (parent != null && (current != parent.Left))
				{
					current = parent;
					parent = parent.Parent;
				}

				return parent; //Nao tem
			}





		}

		public static IEnumerable<GraphNode<int>> BuildOrder(DirectedGraph g)
		{
			//Hashset to verify if node was built
			HashSet<GraphNode<int>> builtNodesVerifier = new HashSet<GraphNode<int>>();
			//List to maitain order of builds. Ideally these two could be merged together in a single class.
			List<GraphNode<int>> builtNodes = new List<GraphNode<int>>();

			HashSet<GraphNode<int>> visitedNodes = new HashSet<GraphNode<int>>();

			foreach (var node in g.Nodes)
				if (!tryAdd(node, builtNodesVerifier, builtNodes, visitedNodes))
					return null;

			return builtNodes;
		}

		private static bool tryAdd(GraphNode<int> n, HashSet<GraphNode<int>> builtNodesVerifier, List<GraphNode<int>> builtNodes, HashSet<GraphNode<int>> visitedNodes)
		{
			if (builtNodesVerifier.Contains(n))
				return true;

			if (visitedNodes.Contains(n))
				return false;
			else
			{
				visitedNodes.Add(n);

				//has dependencies
				if(n.AdjacentNodes.Count > 0)
				{
					foreach(var adjNode in n.AdjacentNodes)
					{
						if(!builtNodesVerifier.Contains(adjNode))
						{ 
							var childResult = tryAdd(adjNode, builtNodesVerifier, builtNodes, visitedNodes);
							if (!childResult)
								return false;
						}
					}
				}

				builtNodes.Add(n);
				builtNodesVerifier.Add(n);
				return true;
			}
		}

		public static TreeNode FirstCommonAncestor(TreeNode root, TreeNode a, TreeNode b)
		{
			bool foundA, foundB;
			TreeNode ancestor;

			firstCommonAncestorAux(root, a, b, out foundA, out foundB, out ancestor);
			return ancestor;
		}
		private static void firstCommonAncestorAux(TreeNode root, TreeNode a, TreeNode b, out bool foundA, out bool foundB, out TreeNode foundCommonAncestor)
		{
			foundA = false;
			foundB = false;
			foundCommonAncestor = null;

			if (root == a)
				foundA = true;

			if (root == b)
				foundB = true;

			if(foundB && foundA)
			{
				foundCommonAncestor = root;
				return;
			}		

			bool leftResultA = false, leftResultB = false;
			TreeNode resultingTreeNode = null;

			//Recursion on the left side
			if(root.Left != null)
				firstCommonAncestorAux(root.Left, a, b, out leftResultA, out leftResultB, out resultingTreeNode);



			if (resultingTreeNode != null)
			{
				foundCommonAncestor = resultingTreeNode;
				return;
			}

			//Recursion on the right side
			bool rightResultA = false, rightResultB = false;
			if (root.Right != null)
				firstCommonAncestorAux(root.Right, a, b, out rightResultA, out rightResultB, out resultingTreeNode);

			if (resultingTreeNode != null)
			{
				foundCommonAncestor = resultingTreeNode;
				return;
			}

			foundA |= rightResultA || leftResultA;
			foundB |= rightResultB || leftResultB;

			//root is A or B and the other element is in left or right subtreee
			if (
				(root == a && (leftResultB || rightResultB)) || //A is root and B child
				(root == b && (leftResultA || rightResultA)) || //B is root and A is child
				(rightResultA && leftResultB) || //B is left and A is right
				(rightResultB && leftResultA)) //B is right and A is left
			{
				foundCommonAncestor = root;
				return;
			}
		}

		public static List<LinkedList<int>> BSTSequence(TreeNode node)
		{
			if (node == null)
				return new List<LinkedList<int>>() { new LinkedList<int>() };

			List<LinkedList<int>> result = new List<LinkedList<int>>();

			var headElement = node.Value;
			List<LinkedList<int>> leftResult = BSTSequence(node.Left);
			List<LinkedList<int>> rightResult = BSTSequence(node.Right);

			foreach(var left in leftResult)
				foreach(var right in rightResult)
				{
					List<LinkedList<int>> weavedLists = new List<LinkedList<int>>();
					weave(left, right, weavedLists);
					weavedLists.ForEach(x => x.AddFirst(headElement));
					result.AddRange(weavedLists);
				}

			return result;
		}
		private static void weave(LinkedList<int> leftResult, LinkedList<int> rightResult, List<LinkedList<int>> results, LinkedList<int> preffix = null)
		{
			if (preffix == null)
				preffix = new LinkedList<int>();

			if(leftResult.Count == 0 || rightResult.Count == 0)
			{
				preffix = new LinkedList<int>(preffix); //clone
				foreach (var el in leftResult)
					preffix.AddLast(el);

				foreach (var el in rightResult)
					preffix.AddLast(el);

				results.Add(preffix);
				return;
			}

			//senao faz recursao adicionando o primeiro de left e depois o primeiro de right
			var leftHead = leftResult.First;
			leftResult.RemoveFirst();
			preffix.AddLast(leftHead);
			weave(leftResult, rightResult, results, preffix);
			preffix.RemoveLast();
			leftResult.AddFirst(leftHead);

			var rightHead = rightResult.First;
			rightResult.RemoveFirst();
			preffix.AddLast(rightHead);
			weave(leftResult, rightResult, results, preffix);
			preffix.RemoveLast();
			rightResult.AddFirst(rightHead);
		}

		public static bool CheckSubtree(TreeNode t1, TreeNode t2)
		{
			if (t1 == null || t2 == null)
				throw new ArgumentNullException();

			Queue<TreeNode> q = new Queue<TreeNode>();
			q.Enqueue(t1);
			while(q.Count > 0)
			{
				var currentNode = q.Dequeue();

				if(currentNode.Value == t2.Value)
				{
					if(compareTree(currentNode, t2))
					{
						return true;
					}
				}

				if (currentNode.Left != null)
					q.Enqueue(currentNode.Left);

				if (currentNode.Right != null)
					q.Enqueue(currentNode.Right);
			}

			return false;
		}

		private static bool compareTree(TreeNode currentNode, TreeNode t2)
		{
			if (currentNode == null && t2 == null)
				return true;
			else
				return 
					currentNode != null && 
					t2 != null && currentNode.Value == t2.Value && 
					compareTree(currentNode.Left, t2.Left) && 
					compareTree(currentNode.Right, t2.Right);
		}

		public static int PathSum(TreeNode n, int targetSum)
		{
			int result = 0;

			Queue<TreeNode> q = new Queue<TreeNode>();
			q.Enqueue(n);

			while(q.Count > 0)
			{
				var currentNode = q.Dequeue();

				sumPathFromRoot(currentNode, 0, targetSum, ref result);

				if (currentNode.Left != null)
					q.Enqueue(currentNode.Left);

				if (currentNode.Right != null)
					q.Enqueue(currentNode.Right);
			}

			return result;
		}

		private static void sumPathFromRoot(TreeNode currentNode, int currentSum, int targetSum, ref int result)
		{
			currentSum += currentNode.Value;
			if (currentSum == targetSum)
				result++;

			if (currentNode.Left != null)
				sumPathFromRoot(currentNode.Left, currentSum, targetSum, ref result);

			if (currentNode.Right != null)
				sumPathFromRoot(currentNode.Right, currentSum, targetSum, ref result);
		}
	}
}
