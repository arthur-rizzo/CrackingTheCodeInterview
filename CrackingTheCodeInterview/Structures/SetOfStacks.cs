using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CrackingTheCodeInterview.Structures
{
    public class SetOfStacks
    {
		private int stackThreshold;

		List<Stack<int>> set;

		public SetOfStacks(int stackThreshold)
		{
			if (stackThreshold <= 0)
				throw new ArgumentOutOfRangeException();

			this.stackThreshold = stackThreshold;
			this.set = new List<Stack<int>>();
		}

		public void Push(int x)
		{
			bool needNewStack = set.Count == 0 || set.Last().Count == stackThreshold;
			if(needNewStack)
			{
				Stack<int> newStack = new Stack<int>();
				newStack.Push(x);
				set.Add(newStack);
			}
			else
				set.Last().Push(x);
		}

		public int Pop()
		{
			if (set.Count == 0)
				throw new InvalidOperationException();

			var lastStack = set.Last();
			var result = lastStack.Pop();

			if (lastStack.Count == 0)
				set.RemoveAt(set.Count - 1);

			return result;
		}

		public int PopAt(int index)
		{
			if (index < (set.Count))
			{
				return set[index].Pop();
			}
			else
				throw new ArgumentOutOfRangeException();
		}

    }
}
