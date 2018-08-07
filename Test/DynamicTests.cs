using CrackingTheCodeInterview;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Test
{
	[TestClass]
    public class DynamicTests
    {
		[TestMethod]
		[DataRow(1, 1)] 
		[DataRow(2, 2)]
		[DataRow(3, 4)]
		[DataRow(4, 10)]
		[DataRow(5, 19)]
		[DataRow(14, 4936)]
		public void ThreeStepsTests(int n, int expected)
		{
			var result = Dynamic.ThreeSteps(n);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void RobotInAGrid()
		{
			int[,] grid = new int[4, 5];
			grid[0, 1] = 1;
			grid[1, 1] = 1;
			grid[2, 1] = 1;
			grid[1, 3] = 1;
			grid[2, 3] = 1;
			grid[3, 3] = 1;
			var result = Dynamic.RobotInAGrid(grid);

			Trace.WriteLine("");
			if(result != null)
			{
				foreach (var node in result)
					Trace.Write(node + "->");
			}
		}

		[TestMethod]
		[DataRow("0", 0)]
		[DataRow("1,2,3,4,5,5", 5)]
		public void MagicIndex(string array, int index)
		{
			var input = array.Split(',').Select(int.Parse).ToArray();
			var result = Dynamic.MagicIndex(input);
			Assert.AreEqual(index, result);
		}

		[TestMethod]
		[DataRow("1",2)]
		[DataRow("1,2", 4)]
		[DataRow("1,2,3", 8)]
		public void PowerSet(string setString, int expectedNumberOfSets)
		{
			HashSet<int> set = new HashSet<int>(setString.Split(',').Select(int.Parse));
			var result = Dynamic.PowerSet(set);

			Assert.AreEqual(result.Count(), expectedNumberOfSets);
		}

		[TestMethod]
		public void HanoiTower()
		{
			for(int i = 1; i < 11; i++)
			{
				Stack<int> s1 = new Stack<int>();
				for (int j = i; j > 0; j--)
					s1.Push(j);

				Stack<int> s2 = new Stack<int>();
				Stack<int> s3 = new Stack<int>();

				Dynamic.HanoiTower(i, s1, s2, s3);

				Assert.IsTrue(s1.Count == 0);
				Assert.IsTrue(s2.Count == 0);

				var el = s3.Pop();
				while(s3.Count > 0)
				{
					var next = s3.Pop();
					if (next < el)
						Assert.Fail();
				}
			}
		}

		[TestMethod]
		[DataRow("abc",6)]
		[DataRow("abcd", 24)]
		[DataRow("abcde", 120)]
		[DataRow("abcdefgh", 40320)]
		public void PermutationWithoutDups(string value, int numberOfPermutations)
		{
			var result = Dynamic.AllPermutationsWithoutDups(value);
			Assert.AreEqual(result.Count, numberOfPermutations);
		}

		[TestMethod]
		[DataRow("abc", 6)]
		[DataRow("abb", 3)]
		[DataRow("aaaaaaa", 1)]
		[DataRow("abcdefgh", 40320)]
		public void PermutationWithDups(string value, int numberOfPermutations)
		{
			var result = Dynamic.AllPermutationsWithDups(value);
			Assert.AreEqual(result.Count, numberOfPermutations);
		}

		[TestMethod]
		[DataRow(1, 1)]
		[DataRow(3, 5)]
		[DataRow(4, 14)]
		[DataRow(5, 42)]
		public void Parentesis(int n, int expectedCount)
		{
			var result = Dynamic.Parentesis(n);
			Assert.AreEqual(expectedCount, result.Count);

			foreach(var s in result)
				Assert.IsTrue(isWellFormed(s));
		}
		private bool isWellFormed(string s)
		{
			Trace.WriteLine(s);
			int count = 0;
			foreach(var c in s)
			{
				if (c == '(')
					count++;
				else if (c == ')')
					count--;
				else
					return false;

				if (count < 0)
					return false;
			}

			return true;
		}


		[TestMethod]
		[DataRow(25,13)]
		public void WaysOfChange(int value, int expectedResult)
		{
			var result = Dynamic.MakeChange(value);
			Assert.AreEqual(expectedResult, result);
		}


		[TestMethod]
		[DataRow(4)]
		[DataRow(8)]
		public void Queens(int n)
		{
			var results = Dynamic.NQueens(n);

			foreach(var result in results)
			{
				Trace.WriteLine("");
				for(int i = 0; i < n; i++)
				{
					Trace.WriteLine("");
					for(int j = 0; j < n; j++)
						Trace.Write(result[i, j] ? "1 " : "0 ");
				}
			}
		}

		[TestMethod]
		[DataRow("1",false, 0)]
		[DataRow("1", true, 1)]
		[DataRow("0", true, 0)]
		[DataRow("0", false, 1)]
		[DataRow("0&0&0&1^1|0", true, 10)]
		public void BooleanEvalutation(string s, bool result, int expected)
		{
			var count = Dynamic.BooleanEvalutation(s, result);
			Assert.AreEqual(expected, count);
		}
	}
}
