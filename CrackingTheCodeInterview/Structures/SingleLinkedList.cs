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
			Head = new MyLinkedListNode<T>(headValue);
		}
		public SingleLinkedList(MyLinkedListNode<T> head)
		{
			Head = head;
		}

		public MyLinkedListNode<T> Head { get; set; }

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

	public class MyLinkedListNode<T>
	{
		public T Value { get; set; }

		public MyLinkedListNode<T> Next { get; set; }

		public MyLinkedListNode(T value)
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
