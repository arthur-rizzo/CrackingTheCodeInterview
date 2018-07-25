using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview.Structures
{
    public class MyQueue
    {
		private Stack<int> s1;
		private Stack<int> s2;

		public MyQueue()
		{
			s1 = new Stack<int>();
			s2 = new Stack<int>();
		}

		public void Enqueue(int value)
		{
			s1.Push(value);
		}

		public int Dequeue()
		{
			if (s2.Count > 0)
				return s2.Pop();
			else
			{
				if(s1.Count > 0)
				{
					int value;
					while (s1.TryPop(out value))
						s2.Push(value);

					return s2.Pop();
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}
