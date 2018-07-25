using CrackingTheCodeInterview.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	[TestClass]
    public class MyQueueTests
    {
		[TestMethod]
		public void MyQueueTest()
		{
			MyQueue q = new MyQueue();

			q.Enqueue(1);
			q.Enqueue(2);
			q.Enqueue(3);
			q.Enqueue(4);

			var x = q.Dequeue();
			Assert.AreEqual(x, 1);

			x = q.Dequeue();
			Assert.AreEqual(x, 2);

			x = q.Dequeue();
			Assert.AreEqual(x, 3);

			q.Enqueue(5);

			x = q.Dequeue();
			Assert.AreEqual(x, 4);

			x = q.Dequeue();
			Assert.AreEqual(x, 5);

			Assert.ThrowsException<InvalidOperationException>(() => q.Dequeue());
		}

    }
}
