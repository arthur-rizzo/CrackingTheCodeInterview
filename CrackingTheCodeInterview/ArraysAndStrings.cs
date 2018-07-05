using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CrackingTheCodeInterview._01._Array_And_Strings
{
	public class ArraysAndStrings
	{
		public static bool IsUnique(string s)
		{
			if (string.IsNullOrEmpty(s))
				return true;

			HashSet<char> charSet = new HashSet<char>();
			foreach (char c in s)
			{
				if (charSet.Contains(c))
					return false;
				else
					charSet.Add(c);
			}
			return true;
		}

		public static bool CheckPermutation(string a, string b)
		{
			if (a == null || b == null)
				return false;

			if (a.Length != b.Length)
				return false;

			Dictionary<char, int> occurrencesCounter = createOcurrencesDictionary(a);

			for (int i = 0; i < b.Length; i++)
			{
				char c = b[i];
				if (occurrencesCounter.ContainsKey(c))
				{
					occurrencesCounter[c]--;
					if (occurrencesCounter[c] < 0)
						return false;
				}
				else
					return false;
			}

			return true;
		}
		private static Dictionary<char, int> createOcurrencesDictionary(string a)
		{
			Dictionary<char, int> occurrencesCounter = new Dictionary<char, int>();

			foreach (var c in a)
			{
				if (occurrencesCounter.ContainsKey(c))
					occurrencesCounter[c]++;
				else
					occurrencesCounter[c] = 1;
			}

			return occurrencesCounter;
		}


	}
}
