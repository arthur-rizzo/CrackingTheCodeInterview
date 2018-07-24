using System;
using CrackingTheCodeInterview._01._Array_And_Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using CrackingTheCodeInterview.Structures;

namespace Test
{
	[TestClass]
	public class LinkedListTests
	{
		[TestMethod]
		[DataRow("1,1,1,2,3,1,4,5,5", "1,2,3,4,5")]
		[DataRow("1,1,1,1,1,1,1","1")]
		[DataRow("1,2,3,4,5", "1,2,3,4,5")]
		[DataRow("2", "2")]
		[DataRow("1,2,3,4,5,6,7,6,5,4,3,2,1", "1,2,3,4,5,6,7")]
		public void RemoveDuplicates(string list, string resultString)
		{
			var input = parse(list);
			print(input);
			var result = parse(resultString);

			LinkedLists.RemoveDuplicates(input);
			print(input);
			AssertAreEqual(input, result);
		}

		[TestMethod]
		[DataRow("1,8,7,6,5,4,3,2,1", 4)]
		[DataRow("9,8,7,6,5,4,3,2,1", 1)]
		[DataRow("9,8,7,6,5,4,3,2,1", 9)]
		[DataRow("2,1", 2)]
		[DataRow("5", 1)]
		[DataRow("5", 6)]
		[DataRow("2,1", 1)]
		public void Partition(string list, int n)
		{
			var linkedList = parse(list);
			print(linkedList);
			LinkedLists.Partition(linkedList, n);
			print(linkedList);
			bool? lower = null;

			var node = linkedList.Head;
			while(node != null)
			{
				if(node.Value < n)
				{
					if (lower == null)
						lower = true;
					else if (lower == false)
						Assert.Fail("found element lower than threshold after higher elements");
				}
				else
				{
					if (lower == null)
						lower = false;
					else if(lower == true)
					{
						lower = false;
					}
				}

				node = node.Next;
			}
		}

		[TestMethod]
		[DataRow("1", "1", "2")]
		[DataRow("5","5","0,1")]
		[DataRow("1,5", "1,7", "2,2,1")]
		[DataRow("1,2,3","1,2","2,4,3")]
		[DataRow("9,9,9", "9,9", "8,9,0,1")]
		public void SumLists(string l1, string l2, string lresult)
		{
			var a = parse(l1);
			var b = parse(l2);
			var expected = parse(lresult);

			var result = LinkedLists.SumLists(a, b);

			AssertAreEqual(expected, result);
		}

		[TestMethod]
		[DataRow("1", true)]
		[DataRow("1,1", true)]
		[DataRow("1,2", false)]
		[DataRow("1,2,1", true)]
		[DataRow("1,2,1,1", false)]
		[DataRow("1,2,3,4,5", false)]
		[DataRow("1,2,3,2,1", true)]
		public void Palindrome(string l1, bool expectedResult)
		{
			var list = parse(l1);
			var actualResult = LinkedLists.Palindrome(list);

			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod]
		public void Intersection()
		{
			var list = parse("1,2,3,4,5,6");
			var list2 = parse("2,2,2,2");
			list2.Head.Next.Next.Next.Next = list.Head.Next.Next;

			var result = LinkedLists.Intersection(list2, list);
			Assert.AreSame(result, list.Head.Next.Next);
		}

		private void AssertAreEqual<T>(SingleLinkedList<T> list1, SingleLinkedList<T> list2)
		{
			if (list1 == null && list2 == null)
			{
				return;
			}
			else if (list1 != null && list2 != null)
			{
				var h1 = list1.Head;
				var h2 = list2.Head;

				do
				{
					if (h1 == null && h2 == null)
						return;
					else if (h1 != null && h2 != null)
					{
						Assert.AreEqual(h1.Value, h2.Value);
					}
					else
						Assert.Fail();

					h1 = h1.Next;
					h2 = h2.Next;
				}
				while (h1 != null || h2 != null);
			}
			else
				Assert.Fail();
		}

		private static SingleLinkedList<int> parse(string s)
		{
			if (string.IsNullOrWhiteSpace(s))
				return null;

			var split = s.Split(',');


			if (split.Length > 0)
			{
				Node<int> head = new Node<int>(int.Parse(split[0]));
				var current = head;
				for (int i = 1; i < split.Length; i++)
				{
					Node<int> node = new Node<int>(int.Parse(split[i]));
					current.Next = node;
					current = node;
				}

				return new SingleLinkedList<int>(head);
			}
			else
				return null;
		}

		private static void print<T>(SingleLinkedList<T> list)
		{
			var x = list.Head;
			while(x != null)
			{
				Trace.Write($"{x.Value} ->");

				x = x.Next;
			}
			Trace.WriteLine("");
		}
	}
}
