using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview.Structures
{
    public class StackMinTracking
    {
		//first value is the value of node.. second is minimum of substack
		Node<Tuple<int,int>> headElement = null;

		public StackMinTracking()
		{

		}

		public void Push(int value)
		{
			var previousHead = headElement;
			int minValue = value;
			if (previousHead != null && minValue > previousHead.Value.Item2)
				minValue = previousHead.Value.Item2;

			headElement = new Node<Tuple<int, int>>(new Tuple<int, int>(value, minValue));
			headElement.Next = previousHead;
		}

		public int Pop()
		{
			if (headElement == null)
				throw new InvalidOperationException();

			var current = headElement;
			headElement = headElement.Next;

			return current.Value.Item1;
		}

		public int Min()
		{
			if (headElement == null)
				throw new InvalidOperationException();

			return headElement.Value.Item2;
		}
    }
}
