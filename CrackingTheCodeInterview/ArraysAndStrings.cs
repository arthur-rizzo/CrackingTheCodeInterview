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

		public static string URLify(string url, int realLength)
		{
			if (realLength < 0)
				throw new ArgumentOutOfRangeException($"{nameof(realLength)} must be greater or equal to zero");
			if (string.IsNullOrWhiteSpace(url))
				return string.Empty;

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < realLength; i++)
			{
				char c = url[i];
				if (c == ' ')
					builder.Append("%20");
				else
					builder.Append(c);
			}

			return builder.ToString();
		}

		public static bool IsPalindromePermutation(string s)
		{
			if (string.IsNullOrEmpty(s))
				return true;

			s = s.ToLowerInvariant();
			Dictionary<char, int> occurences = new Dictionary<char, int>();
			foreach (char c in s)
			{
				if (occurences.ContainsKey(c))
					occurences[c]++;
				else
					occurences[c] = 1;
			}

			//Se uma string tem tamanho impar, teremos X
			int requiredEvenCharacters = s.Length % 2 == 0 ? occurences.Keys.Count : occurences.Keys.Count - 1;
			var evenCharacters = occurences.Values.Where(c => c % 2 == 0).Count();

			return requiredEvenCharacters == evenCharacters;
		}

		public static bool OneEditAway(string s1, string s2)
		{
			if (s1 == null)
				s1 = "";

			if (s2 == null)
				s2 = "";

			//Could be one replacement away
			if (s2.Length == s1.Length)
			{
				int differences = 0;
				for (int i = 0; i < s2.Length; i++)
				{
					if (s1[i] != s2[i])
						differences++;
				}

				return differences <= 1;
			}
			else if (Math.Abs(s1.Length - s2.Length) > 1) //a diferenca de tamanho já é maior que 1;
				return false;
			else //1 char difference (adding to the smaller means the same than to remove from the bigger)
			{
				string bigger, smaller;
				extractBiggerAndSmallerStrings(s1, s2, out bigger, out smaller);

				int indexSmaller = 0;
				int differencesCount = 0;
				for (int indexBigger = 0; indexBigger < bigger.Length; indexBigger++)
				{
					if (indexSmaller < smaller.Length && bigger[indexBigger] == smaller[indexSmaller])
					{
						indexSmaller++;
						continue;
					}
					else
					{
						differencesCount++;
						if (differencesCount > 1)
							break;

						continue;
					}
				}

				return differencesCount <= 1;
			}

		}
		private static void extractBiggerAndSmallerStrings(string s1, string s2, out string bigger, out string smaller)
		{
			smaller = "";
			if (s1.Length > s2.Length)
			{
				bigger = s1;
				smaller = s2;
			}
			else
			{
				bigger = s2;
				smaller = s1;
			}
		}

		public static string StringCompression(string s)
		{
			if (string.IsNullOrEmpty(s))
				return string.Empty;

			int counter = 0;
			char currentChar = ' ';

			StringBuilder builder = new StringBuilder();

			foreach (char c in s)
			{
				if (currentChar == ' ')
					currentChar = c;

				if (c == currentChar)
					counter++;
				else
				{
					builder.Append(currentChar + counter.ToString());

					currentChar = c;
					counter = 1;
				}
			}

			builder.Append(currentChar + counter.ToString());

			var result = builder.ToString();
			if (result.Length >= s.Length)
				result = s;

			return result;
		}

		private static void matrixRotation(int[][] matrix, int diagonalIndex)
		{
			int lineSize = matrix.Length - (diagonalIndex * 2);
			if (lineSize <= 1 || diagonalIndex < 0)
				return;

			//faz o trocandinho do subquadrado iniciado na coordenada (x,x) onde x é diagonalIndex
			for (int lineIndex = 0; lineIndex < (lineSize - 1); lineIndex++)
			{
				//Performs 4 swaps in the following order: top > left > bottom > right
				//defines the 4 elements positions to be rotated in the current iteration
				var topElementX = 0 + diagonalIndex;
				var topElementY = lineIndex + diagonalIndex;

				var leftElementX = ((lineSize - lineIndex) - 1) + diagonalIndex;
				var leftElementY = 0 + diagonalIndex;

				var bottomElementX = (lineSize - 1) + diagonalIndex;
				var bottomElementY = ((lineSize - lineIndex) - 1) + diagonalIndex;

				var rightElementX = lineIndex + diagonalIndex;
				var rightElementY = (lineSize - 1) + diagonalIndex;

				//defines the elements in variables and perform swaps
				var topElement = matrix[topElementX][topElementY];
				var leftElement = matrix[leftElementX][leftElementY];
				var bottomElement = matrix[bottomElementX][bottomElementY];
				var rightElement = matrix[rightElementX][rightElementY];

				matrix[leftElementX][leftElementY] = topElement;
				matrix[bottomElementX][bottomElementY] = leftElement;
				matrix[rightElementX][rightElementY] = bottomElement;
				matrix[topElementX][topElementY] = rightElement;
			}

			matrixRotation(matrix, diagonalIndex - 1);
		}
		public static void MatrixRotation(int[][] matrix)
		{
			int diagonalSize = matrix.Length;
			int diagonalEndIndex = (diagonalSize / 2) - 1;

			matrixRotation(matrix, diagonalEndIndex);
		}

		public static void ZeroMatrix(int[][] matrix)
		{
			HashSet<int> rows = new HashSet<int>();
			HashSet<int> columns = new HashSet<int>();

			for (int i = 0; i < matrix.Length; i++)
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					if (matrix[i][j] == 0)
					{
						rows.Add(i);
						columns.Add(j);
					}
				}
			}

			foreach(var x in rows)
			{
				for (int i = 0; i < matrix[0].Length; i++)
					matrix[x][i] = 0;
			}

			foreach (var x in columns)
			{
				for (int i = 0; i < matrix.Length; i++)
					matrix[i][x] = 0;
			}

		}

		public static bool IsRotation(string s1, string s2)
		{
			string comparerString = s2 + s2;
			return 
				s1.Length == s2.Length &&
				comparerString.Contains(s1);
		}
	}
}
