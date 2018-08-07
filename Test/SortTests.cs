using CrackingTheCodeInterview;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
	[TestClass]
    public class SortTests
    {
		[TestMethod]
		[DataRow("1")]
		[DataRow("3,2,1")]
		[DataRow("4,3,1,2")]
		[DataRow("2,1")]
		public void BubbleSort(string list)
		{
			var array = list.Split(",").Select(int.Parse).ToArray();
			Sorting.BubbleSort(array);
			assertArraySorted(array);
		}

		[TestMethod]
		[DataRow("1")]
		[DataRow("3,2,1")]
		[DataRow("4,3,1,2")]
		[DataRow("2,1")]
		public void MergeSort(string list)
		{
			var array = list.Split(",").Select(int.Parse).ToArray();
			Sorting.MergeSort(array);
			assertArraySorted(array);
		}

		[TestMethod]
		[DataRow("1")]
		[DataRow("3,2,1")]
		[DataRow("4,3,1,2")]
		[DataRow("2,1")]
		public void QuickSort(string list)
		{
			var array = list.Split(",").Select(int.Parse).ToArray();
			Sorting.QuickSort(array);
			assertArraySorted(array);
		}

		private static void assertArraySorted(int[] array)
		{
			for (int i = 0; i < array.Length - 1; i++)
				if (array[i] > array[i + 1])
					Assert.Fail();
		}


	}
}
