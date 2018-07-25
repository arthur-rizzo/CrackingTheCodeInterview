using CrackingTheCodeInterview.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	[TestClass]
    public class StackMinTrackingTests
    {
		[TestMethod]
		public void MinTrackingTests()
		{
			StackMinTracking s = new StackMinTracking();

			s.Push(10);
			Assert.AreEqual(10, s.Min());

			s.Push(12);
			Assert.AreEqual(10, s.Min());

			s.Push(9);
			Assert.AreEqual(9, s.Min());

			var x = s.Pop();
			Assert.AreEqual(9, x);
			Assert.AreEqual(10, s.Min());

			x = s.Pop();
			Assert.AreEqual(12, x);

			x = s.Pop();
			Assert.AreEqual(10, x);

			s.Push(10);
			s.Push(9);
			s.Push(100);
			s.Push(8);
			s.Push(10);
			s.Push(10);

			Assert.AreEqual(8, s.Min());
		}
	}
}
