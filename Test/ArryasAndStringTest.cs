using CrackingTheCodeInterview._01._Array_And_Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
	}
}
