using CrackingTheCodeInterview.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
	[TestClass]
    public class SetOfStacksTests
	{
		[TestMethod]
		public void SetOfStacksTest()
		{
			SetOfStacks s = new SetOfStacks(3);

			s.Push(1); s.Push(1); s.Push(1);
			s.Push(2); s.Push(2); s.Push(2);
			s.Push(3); s.Push(3); s.Push(3);

			var x = s.Pop();
			Assert.AreEqual(x, 3);

			s.Pop(); s.Pop();
			x = s.Pop();
			Assert.AreEqual(x, 2);

			s.Push(2); s.Push(3); s.Push(3); s.Push(3);

			x = s.PopAt(0);
			Assert.AreEqual(x, 1);

			x = s.PopAt(1);
			Assert.AreEqual(x, 2);

			x = s.PopAt(2);
			Assert.AreEqual(x, 3);

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => s.PopAt(4));
		}
	}
}
