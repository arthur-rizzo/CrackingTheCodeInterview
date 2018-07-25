using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CrackingTheCodeInterview;

namespace Test
{
	[TestClass]
	public class StacksAndQueuesTests
	{
		[TestMethod]
		[DataRow("1")]
		[DataRow("1,2")]
		[DataRow("1,2,3,4,5,6")]
		[DataRow("1,1,1,1,1")]
		[DataRow("6,5,4,3,2,1,7")]
		[DataRow("5,6,4,2,3,1")]
		public void SortStackTest(string input)
		{
			var stack = parse(input);

			StacksAndQueues.SortStack(stack);

			int currentElement = int.MinValue;
			while(stack.Count > 0)
			{
				var element = stack.Pop();
				if (currentElement > element)
					Assert.Fail();

				currentElement = element;
			}
		}


		private Stack<int> parse(string s)
		{
			Stack<int> r = new Stack<int>();
			s.Split(',').ToList().ForEach(x => r.Push(int.Parse(x)));
			return r;
		}
    }
}
