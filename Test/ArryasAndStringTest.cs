using System;
using CrackingTheCodeInterview._01._Array_And_Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public class ArryasAndStringTest
    {
        [TestMethod]
		[DataRow(null, true)]
		[DataRow("", true)]
		[DataRow("a", true)]
		[DataRow("abcdefa", false)]
		[DataRow("aa", false)]
		[DataRow("gabirus", true)]
		public void IsUnique(string a, bool expected)
        {
			Assert.AreEqual(expected, ArraysAndStrings.IsUnique(a));
        }

		[TestMethod]
		[DataRow(null, null, false)]
		[DataRow("", null, false)]
		[DataRow(null, "", false)]
		[DataRow("", "", true)]
		[DataRow("aba", "baa", true)]
		[DataRow("gabirusss", "sssgabiur", true)]
		[DataRow("aabbaaa", "babaaaaa", false)]
		public void IsPermutation(string a, string b, bool expected)
		{
			Assert.AreEqual(expected, ArraysAndStrings.CheckPermutation(a, b));
		}

		[TestMethod]
		[DataRow(null, 0, "")]
		[DataRow("", 0, "")]
		[DataRow(" ", 0, "")]
		[DataRow("   ", 0, "")]
		[DataRow("a a  ", 3, "a%20a")]
		public void URLfy(string s, int length, string result)
		{
			Assert.AreEqual(result, ArraysAndStrings.URLify(s, length));
		}

		[TestMethod]
		[DataRow("", true)]
		[DataRow("a", true)]
		[DataRow("   ", true)]
		[DataRow("aa ", true)]
		[DataRow("bb  ccc", true)]
		[DataRow("aoiuuioa22xyy", true)]
		[DataRow("aoiuuioa222xyy", false)]
		public void IsPalindromePermutation(string s, bool result)
		{
			Assert.AreEqual(result, ArraysAndStrings.IsPalindromePermutation(s));
		}

		[TestMethod]
		[DataRow("", " ", true)]
		[DataRow(" ", "", true)]
		[DataRow("a", " ", true)]
		[DataRow("", "a", true)]
		[DataRow(" a", " ", true)]
		[DataRow("a ", " ", true)]
		[DataRow("caralho", "caralh", true)]
		[DataRow("caralho", "aralho", true)]
		[DataRow("caralo", "caralho", true)]
		[DataRow("caralbo", "caralho", true)]
		[DataRow("   b ", "   ", false)]
		[DataRow("xyzh", "xyzha", true)]
		[DataRow(" ", "   ", false)]
		[DataRow("caralbo", "caralho ", false)]
		public void OneEditAway(string s1, string s2, bool result)
		{
			Assert.AreEqual(result, ArraysAndStrings.OneEditAway(s1,s2));
		}

		[TestMethod]
		[DataRow("", "")]
		[DataRow(null, "")]
		[DataRow("a", "a")]
		[DataRow("ab", "ab")]
		[DataRow("aa", "aa")]
		[DataRow("aab", "aab")]
		[DataRow("aabbb", "a2b3")]
		[DataRow("aaabbbaaaaacde", "a3b3a5c1d1e1")]
		public void StringCompression(string input, string result)
		{
			Assert.AreEqual(result, ArraysAndStrings.StringCompression(input));
		}

		[TestMethod]
		[DataRow("1","1")]
		[DataRow("1,2|3,4", "2,4|1,3")]
		[DataRow("10,11,12,13,14|15,16,17,18,19|20,21,22,23,24|25,26,27,28,29|30,31,32,33,34", "14,19,24,29,34|13,18,23,28,33|12,17,22,27,32|11,16,21,26,31|10,15,20,25,30")]
		public void MatrixRotation(string inputString, string resultString)
		{
			int[][] input = convertToArray(inputString);
			Trace.WriteLine("Input:");
			printMatrix(input);

			ArraysAndStrings.MatrixRotation(input);

			Trace.WriteLine("Result");
			printMatrix(input);

			int[][] result = convertToArray(resultString);
			for (int i = 0; i < input.Length; i++)
				for(int j = 0; j < input[i].Length; j++)
					Assert.AreEqual(input[i][j], result[i][j]);
		}

		[TestMethod]
		[DataRow("1", "1")]
		[DataRow("1,2|3,0", "1,0|0,0")]
		[DataRow("1,2,1|3,0,2|1,1,1", "1,0,1|0,0,0|1,0,1")]
		public void ZeroMatrix(string inputString, string resultString)
		{
			int[][] input = convertToArray(inputString);
			Trace.WriteLine("Input:");
			printMatrix(input);

			ArraysAndStrings.ZeroMatrix(input);

			Trace.WriteLine("Result");
			printMatrix(input);

			int[][] result = convertToArray(resultString);
			for (int i = 0; i < input.Length; i++)
				for (int j = 0; j < input[i].Length; j++)
					Assert.AreEqual(input[i][j], result[i][j]);
		}

		private void printMatrix(int[][] input)
		{
			foreach (var i in input)
			{
				foreach(var j in i)
				{
					Trace.Write(j);
					Trace.Write(" ");
				}
				Trace.WriteLine("");
			}
		}
		private int[][] convertToArray(string v)
		{
			return v.Split('|').Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();
		}

		[TestMethod]
		[DataRow("","",true)]
		[DataRow("a", "aa", false)]
		[DataRow("ab", "ab", true)]
		[DataRow("abcdef", "bcdefa", true)]
		[DataRow("abcdef", "bcdef", false)]
		[DataRow("abcbcdef", "bcdefabc", true)]
		public void IsRotation(string s1, string s2, bool expected)
		{
			bool result = ArraysAndStrings.IsRotation(s1, s2);
			Assert.AreEqual(expected, result);
		}
	}
}
