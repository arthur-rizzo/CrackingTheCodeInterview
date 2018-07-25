using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview
{
    public class StacksAndQueues
    {
		public static void SortStack(Stack<int> s)
		{
			Stack<int> auxStack = new Stack<int>();


			while(s.Count > 0)
			{
				int current = s.Pop();

				while(auxStack.Count > 0 && auxStack.Peek() > current)
				{
					var auxValue = auxStack.Pop();
					s.Push(auxValue);
				}

				auxStack.Push(current);
			}

			while(auxStack.Count > 0)
				s.Push(auxStack.Pop());
		}
    }
}
