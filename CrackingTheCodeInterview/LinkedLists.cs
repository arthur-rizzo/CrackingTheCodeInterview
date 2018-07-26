using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CrackingTheCodeInterview.Structures;

namespace CrackingTheCodeInterview._01._Array_And_Strings
{
	public class LinkedLists
	{
		public static void RemoveDuplicates(SingleLinkedList<int> list)
		{
			if (list == null)
				throw new ArgumentNullException(nameof(list));

			HashSet<int> values = new HashSet<int>();

			Structures.MyLinkedListNode<int> p1 = list.Head;
			Structures.MyLinkedListNode<int> p2 = p1.Next;

			values.Add(p1.Value);

			while(p2 != null)
			{
				if(values.Contains(p2.Value))
				{
					p2 = p2.Next;
				}
				else
				{
					values.Add(p2.Value);

					p1.Next = p2;
					p1 = p2;
					p2 = p1.Next;
				}
			}

			p1.Next = p2;
		}

		public static int KthToLast(int k, SingleLinkedList<int> list)
		{
			var x = list.Head;
			var y = list.Head;

			for (int i = 0; i < k - 1; i++)
				y = y.Next;

			while(y != null)
			{
				y = y.Next;
				x = x.Next;
			}

			return x.Value;
		}

		public static void DeleteMiddleNode(Structures.MyLinkedListNode<int> node)
		{
			if (node == null || node.Next == null)
				throw new ArgumentException();

			node.Value = node.Next.Value;
			node.Next = node.Next.Next;
		}

		public static void Partition(SingleLinkedList<int> list, int value)
		{
			Structures.MyLinkedListNode<int> previousNode = null;
			Structures.MyLinkedListNode<int> head = list.Head;
			var currentNode = list.Head;

			while(currentNode != null)
			{
				//promotes to head
				if(currentNode.Value < value)
				{
					var nextNode = currentNode.Next;

					if (currentNode != head)
					{ 
						currentNode.Next = head; //sets current element as head
						head = currentNode;
					}

					currentNode = nextNode; //next element is the original next before switching current to head

					if (previousNode != null)
						previousNode.Next = nextNode; //links previous with next
				}
				else
				{ 
					previousNode = currentNode;
					currentNode = currentNode.Next;
				}
			}

			list.Head = head;
		}

		public static SingleLinkedList<int> SumLists(SingleLinkedList<int> a, SingleLinkedList<int> b)
		{
			if (a == null && b == null)
				return null;
			else if (a != null && b == null)
				return a;
			else if (b != null && a == null)
				return b;

			SingleLinkedList<int> result = new SingleLinkedList<int>();

			var e1 = a.Head;
			var e2 = b.Head;
			bool carryOver = false;

			Structures.MyLinkedListNode<int> currentNode = null;
			do
			{
				int sum = 0;
				if (carryOver)
					sum++;

				if (e1 != null)
					sum += e1.Value;

				if (e2 != null)
					sum += e2.Value;

				int newNodeValue = sum;
				carryOver = false;

				if(sum > 9)
				{
					carryOver = true;
					newNodeValue = sum % 10;
				}

				Structures.MyLinkedListNode<int> n = new Structures.MyLinkedListNode<int>(newNodeValue);
				if (currentNode == null)
					result.Head = n;
				else
					currentNode.Next = n;

				currentNode = n;
				e1 = e1?.Next;
				e2 = e2?.Next;
			}
			while (e1 != null || e2 != null || carryOver);

			return result;
		}

		public static bool Palindrome(SingleLinkedList<int> list)
		{
			if (list == null)
				throw new ArgumentNullException();

			Stack<int> stack = new Stack<int>();
			Queue<int> queue = new Queue<int>();

			bool result = true;

			var node = list.Head;

			while (node != null)
			{
				stack.Push(node.Value);
				queue.Enqueue(node.Value);

				node = node.Next;
			}

			while(result && (queue.Count > 0 && stack.Count > 0))
			{
				var a = queue.Dequeue();
				var b = stack.Pop();

				result &= a == b;
			}

			return result;
		}

		public static MyLinkedListNode<int> Intersection(SingleLinkedList<int> a, SingleLinkedList<int> b)
		{
			int aLenght; Structures.MyLinkedListNode<int> aLastElement;
			GetLengthAndLastElement(a, out aLenght, out aLastElement);

			int bLenght; Structures.MyLinkedListNode<int> bLastElement;
			GetLengthAndLastElement(b, out bLenght, out bLastElement);

			Structures.MyLinkedListNode<int> intersection = null;

			//Significa que há interseção, pois o ultimo elemento é o mesmo
			if(object.ReferenceEquals(aLastElement, bLastElement))
			{
				//resta achar o elemento
				//If the lists have different sizes, skips X nodes on the longer list, where X is the size difference
				Structures.MyLinkedListNode<int> aNode = a.Head;
				Structures.MyLinkedListNode<int> bNode = b.Head;

				if (aLenght > bLenght)
					for (int i = 0; i < aLenght - bLenght; i++)
						aNode = aNode.Next;
				else if(bLenght > aLenght)
					for (int i = 0; i < bLenght - aLenght; i++)
						bNode = bNode.Next;

				while(aNode != null && bNode != null)
				{
					if(object.ReferenceEquals(aNode,bNode))
					{
						intersection = aNode;
						break;
					}

					aNode = aNode.Next;
					bNode = bNode.Next;
				}
			}

			return intersection;
		}
		private static void GetLengthAndLastElement(SingleLinkedList<int> a, out int aLenght, out Structures.MyLinkedListNode<int> aLastElement)
		{
			aLenght = 0;
			var aNode = a.Head;
			aLastElement = null;
			while (aNode != null)
			{
				if (aNode.Next == null)
					aLastElement = aNode;

				aLenght++;
				aNode = aNode.Next;
			}
		}

		public MyLinkedListNode<int> LoopDetection(SingleLinkedList<int> list)
		{
			Structures.MyLinkedListNode<int> result = null;

			HashSet<Structures.MyLinkedListNode<int>> visitedNodes = new HashSet<Structures.MyLinkedListNode<int>>();
			var currentNode = list.Head;
			while(currentNode != null)
			{
				if(visitedNodes.Contains(currentNode))
				{
					result = currentNode;
					break;
				}
				else
				{
					visitedNodes.Add(currentNode);
					currentNode = currentNode.Next;
				}
			}

			return result;
		}

		public MyLinkedListNode<int> LoopDetectionWithoutSpace(SingleLinkedList<int> list)
		{
			//based on Floyd loop detection algorithm
			var slow = list.Head;
			var fast = list.Head;

			bool foundLoop = false;
			while (slow != null && fast.Next != null)
			{
				if(slow == fast)
				{
					//they met somewhere inside the cycle.
					//The pointer now is X steps away from the start of the cycle.
					//X is the number of elements in the list before the cycle starts
					foundLoop = true;
					break;
				}

				slow = slow.Next;
				fast = fast.Next.Next;
			}

			if (!foundLoop)
				return null;

			//Since there is a loop, we now set slow to head and move both slow and fast by 1 until they meet
			//They will meet once X steps are taken, meaning we are at the start of the cycle
			slow = list.Head;

			while(slow != fast)
			{
				slow = slow.Next;
				fast = fast.Next;
			}

			return slow; //or fast.. they are the same now.
		}
	}
}
