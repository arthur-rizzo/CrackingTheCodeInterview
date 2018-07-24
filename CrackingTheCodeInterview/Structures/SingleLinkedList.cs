using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview.Structures
{
    public class SingleLinkedList<T>
    {
		public SingleLinkedList() { }
		public SingleLinkedList(T headValue)
		{
			Head = new Node<T>(headValue);
		}
		public SingleLinkedList(Node<T> head)
		{
			Head = head;
		}

		public Node<T> Head { get; set; }

		public override string ToString()
		{
			StringBuilder b = new StringBuilder();

			var node = Head;
			while(node != null)
			{
				b.Append(node.Value + (node.Next != null ? " -> " :""));
			}

			return b.ToString();
		}
	}

	public class Node<T>
	{
		public T Value { get; set; }

		public Node<T> Next { get; set; }

		public Node(T value)
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
