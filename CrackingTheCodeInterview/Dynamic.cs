using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CrackingTheCodeInterview
{
    public class Dynamic
    {



		public static int ThreeSteps(int n)
		{
			int[] cache = new int[n];
			return threeSteps(n, cache);
		}
		private static int threeSteps(int n, int[] cache)
		{
			if (n <= 0)
				return 0;
			else if (n == 1)
				return 1;
			else if (n == 2)
				return 2;
			else if (n == 3)
				return 4;

			if (cache[n-1] == 0)
			{
				cache[n-1] = 3 + threeSteps(n - 1, cache) + threeSteps(n - 2, cache) + threeSteps(n - 3, cache);
			}

			return cache[n-1];
		}


		public enum Step
		{
			Right,
			Down,
			Left,
			Up
		}
		public static LinkedList<Step> RobotInAGrid(int[,] grid)
		{
			//Assumes the grid has value 1 for obstacle and 0 for walkable

			//False means there is no way from there. //null means not evaluated. //linkedList represents the way from there
			object[,] partialResults = new object[grid.GetLength(0), grid.GetLength(1)];

			var result = robotInAGrid(grid, 0, 0, grid.GetLength(0), grid.GetLength(1), partialResults, Step.Left);
			return result as LinkedList<Step>;
		}
		private static object robotInAGrid(int[,] grid, int rowIndex, int columnIndex, int rows, int columns, object[,] partialResults, Step comingFrom)
		{
			if (rowIndex == rows - 1 && columnIndex == columns - 1)
				return new LinkedList<Step>();

			if(partialResults[rowIndex,columnIndex] == null)
			{
				bool foundWay = false;
				foreach(Step s in Enum.GetValues(typeof(Step)))
				{
					//We dont want to evaluate the node from which we came from
					//Nor do we want to go to obstacle nodes or outside the grid boundaries
					int newRow;
					int newColumn;

					if (s != comingFrom && isWalkable(grid, rowIndex, columnIndex, s, out newRow, out newColumn))
					{
						Step newComingFrom = comesFrom(s);
						var result = robotInAGrid(grid, newRow, newColumn, rows, columns, partialResults, newComingFrom);
						if (object.Equals(result, false))
						{
							continue; //There is no way going on S direction
						}
						else if (result is LinkedList<Step> steps)
						{
							steps = new LinkedList<Step>(steps);
							steps.AddFirst(s);
							partialResults[rowIndex, columnIndex] = steps;
							foundWay = true;
							break;
						}
					}
				}

				//Did not find any way going left right, x, whatever
				if(!foundWay)
					partialResults[rowIndex, columnIndex] = false;
			}

			return partialResults[rowIndex, columnIndex];
		}
		private static Step comesFrom(Step s)
		{
			Step newComingFrom = 0;
			switch (s)
			{
				case Step.Right:
					newComingFrom = Step.Left;
					break;
				case Step.Down:
					newComingFrom = Step.Up;
					break;
				case Step.Left:
					newComingFrom = Step.Right;
					break;
				case Step.Up:
					newComingFrom = Step.Down;
					break;
			}

			return newComingFrom;
		}
		private static bool isWalkable(int[,] grid, int startRow, int startColumn, Step step, out int resultRow, out int resultColumn)
		{
			resultColumn = startColumn;
			resultRow = startRow;

			switch (step)
			{
				case Step.Right:
					resultColumn++;
					break;
				case Step.Left:
					resultColumn--;
					break;
				case Step.Down:
					resultRow++;
					break;
				case Step.Up:
					resultRow--;
					break;
				default:
					break;
			}

			if (resultRow >= grid.GetLength(0) || resultColumn >= grid.GetLength(1) || resultRow < 0 || resultColumn < 0)
				return false;
			else
				return grid[resultRow, resultColumn] == 0;
		}

		public static int MagicIndex(int[] values)
		{
			return magicIndex(values, 0, values.Length - 1);
		}
		private static int magicIndex(int[] values, int start, int end)
		{
			if (start < 0 || start > values.Length - 1 || end < 0 || end > values.Length - 1)
				return -1;

			if (start == end)
				return values[start] == start ? start : -1;

			int midPoint = (start + end) / 2;
			if (midPoint == values[midPoint])
				return midPoint;
			else
			{
				//searches on the left
				var leftResult = magicIndex(values, 0, Math.Min(midPoint - 1, values[midPoint]));
				if (leftResult > -1)
					return leftResult;

				// searches on the right
				 var rightResult = magicIndex(values, Math.Max(midPoint + 1, values[midPoint]), end);
				if (rightResult > -1)
					return rightResult;
			}

			return -1;
		}


		public static IEnumerable<ISet<int>> PowerSet(ISet<int> s)
		{
			List<ISet<int>> result = new List<ISet<int>>();

			//Starts with empty set
			result.Add(new HashSet<int>());

			//Foreach element, foreach existing set in the result, clones and adds current element.
			//Adds all altered clones to the result.
			foreach(var element in s)
			{
				var newList = new List<ISet<int>>();
				foreach(var set in result)
				{
					var newSet = new HashSet<int>(set); //clones
					newSet.Add(element); //
					newList.Add(newSet);
				}

				result.AddRange(newList);
			}

			return result;
		}

		public static void HanoiTower(int n, Stack<int> s1, Stack<int> s2, Stack<int> s3)
		{
			if (n <= 0)
				return;

			//move n-1 items from s1 to the s2 (s3 acts as a buffer in this call)
			HanoiTower(n - 1, s1, s3, s2);
			//moves top of s1 to s3
			s3.Push(s1.Pop());
			//moves n-1 items from s2 to s3 using s1 as a buffer
			HanoiTower(n - 1, s2, s1, s3);
		}

		public static List<string> AllPermutationsWithoutDups(string s)
		{
			List<string> result = new List<string>();

			if (s.Length == 1)
				result.Add(s);
			else
			{
				foreach(char c in s)
				{
					string sWithoutC = s.Remove(s.IndexOf(c),1);
					var permutationsWihtoutC = AllPermutationsWithoutDups(sWithoutC);
					permutationsWihtoutC.ForEach(p => result.Add(c + p));
				}
			}

			return result;
		}

		public static List<string> AllPermutationsWithDups(string s)
		{
			List<string> result = new List<string>();
			var charCount = createCharCount(s);
			return allPermutationWithDups(charCount);
		}

		private static List<string> allPermutationWithDups(Dictionary<char, int> charCount)
		{
			List<string> result = new List<string>();

			foreach (var pair in charCount)
			{
				if (pair.Value == 0)
					continue;

				char c = pair.Key;
				Dictionary<char, int> d = new Dictionary<char, int>(charCount);
				d[c]--;

				var partialResult = allPermutationWithDups(d);
				result.AddRange(partialResult.Select(x => c + x));
			}

			if (result.Count == 0)
				result.Add("");

			return result;
		}

		private static Dictionary<char,int> createCharCount(string s)
		{
			Dictionary<char, int> charCount = new Dictionary<char, int>();
			foreach (char c in s)
			{
				if (charCount.ContainsKey(c))
					charCount[c]++;
				else
					charCount[c] = 1;
			}

			return charCount;
		}

		public static List<string> Parentesis(int n)
		{
			int openCount = n;
			int closeCount = 0;

			return Parentesis(openCount, closeCount);
		}
		private static List<string> Parentesis(int openAvailable, int closeAvailable)
		{
			//base case
			if (openAvailable == 0 && closeAvailable == 0)
				return new List<string>() { "" };

			List<string> result = new List<string>();

			if (openAvailable > 0)
			{
				var partialResults = Parentesis(openAvailable - 1, closeAvailable + 1);
				result.AddRange(partialResults.Select(x => "(" + x));
			}

			if (closeAvailable > 0)
			{
				var partialResults = Parentesis(openAvailable, closeAvailable - 1);
				result.AddRange(partialResults.Select(x => ")" + x));
			}

			return result;
		}


		public static void PaintFill(int[,] board, int color, int row, int column)
		{
			if (row < 0 || row > board.GetLength(0) || column < 0 || column > board.GetLength(1))
				return;

			int previusColor = board[row, column];
			HashSet<int> visited = new HashSet<int>();
			PaintFill(board, color, row, column, previusColor, visited);
		}
		private static void PaintFill(int[,] board, int color, int row, int column, int previusColor, HashSet<int> visited)
		{
			if (row < 0 || row > board.GetLength(0) || column < 0 || column > board.GetLength(1))
				return;

			//Se ja visitou o no, retorna
			int index = (board.GetLength(1) * row) + column;
			if (visited.Contains(index))
				return;

			visited.Add(index);

			if(board[row,column] == previusColor)
			{
				board[row, column] = color;
				PaintFill(board, color, row + 1, column, previusColor, visited);
				PaintFill(board, color, row - 1, column, previusColor, visited);
				PaintFill(board, color, row, column + 1, previusColor, visited);
				PaintFill(board, color, row, column - 1, previusColor, visited);
			}
		}

		public static int MakeChange(int n)
		{
			int[] denoms = { 25, 10, 5, 1 };
			int[,] map = new int[n + 1, denoms.Length];
			return makeChange(n, denoms, 0, map);
			 }
		private static int makeChange(int amount, int[] denoms, int index, int[,] map)
		{
			if (map[amount,index] > 0)
			{
				return map[amount,index];
			}

			if (index >= denoms.Length - 1) 
				return 1;
			
			int denomAmount = denoms[index];
			int ways = 0;
			for (int i = 0; i * denomAmount <= amount; i++)
			{
				int amountRemaining = amount - i * denomAmount;
				ways += makeChange(amountRemaining, denoms, index + 1, map);
			}

			map[amount,index] = ways;
			return ways;
		}


		public static List<bool[,]> NQueens(int n)
		{
			List<bool[,]> result = new List<bool[,]>();

			bool[,] board = new bool[n, n];
			HashSet<int> takenRows = new HashSet<int>(n);
			HashSet<int> takenColumns = new HashSet<int>(n);
			HashSet<int> takenDiag1 = new HashSet<int>(n); //diag of differences
			HashSet<int> takenDiag2 = new HashSet<int>(n); //diag of sum

			placeNQueenFromRow(n, 0, board, takenRows, takenColumns, takenDiag1, takenDiag2, result);

			return result;
		}

		private static void placeNQueenFromRow(int numberOfQueens, int row, bool[,] board, HashSet<int> takenRows, HashSet<int> takenColumns, HashSet<int> takenDiag1, HashSet<int> takenDiag2, List<bool[,]> result)
		{
			//Caso base
			if (numberOfQueens == 0)
				result.Add((bool[,])board.Clone());

			for(int column = 0; column < board.GetLength(1); column++)
			{
				if(takenColumns.Contains(column) || takenRows.Contains(row) || takenDiag1.Contains(row - column) || takenDiag2.Contains(row + column))
				{
					continue;
				}

				//adds to the board and to the trackers.
				board[row, column] = true;
				takenRows.Add(row);
				takenColumns.Add(column);
				takenDiag1.Add(row - column);
				takenDiag2.Add(row + column);

				placeNQueenFromRow(numberOfQueens - 1, row + 1, board, takenRows, takenColumns, takenDiag1, takenDiag2, result);
				
				//removes from the board and to the trackers.
				board[row, column] = false;
				takenRows.Remove(row);
				takenColumns.Remove(column);
				takenDiag1.Remove(row - column);
				takenDiag2.Remove(row + column);
			}	
		}




		public class Box
		{
			public int Width { get; set; }
			public int Heigh { get; set; }
			public int Depth { get; set; }

			public Box(int width, int heigh, int depth)
			{
				Width = width;
				Heigh = heigh;
				Depth = depth;
			}

			public override bool Equals(object obj)
			{
				Box b = obj as Box;
				return b != null && this.Depth == b.Depth && this.Width == b.Width && this.Heigh == b.Heigh;
			}

			public override int GetHashCode()
			{
				int x = 43;
				return (7 * Width + 11 * Heigh + 17 * Depth) % x;
			}
		}

		public static int MaxStackHeight(List<Box> boxes)
		{
			return maxStackHeight(new HashSet<Box>(boxes), null);
		}
		private static int maxStackHeight(HashSet<Box> boxes, Box topBox)
		{
			int value = 0;

			var compatibleBoxes = boxes.Where(x => canPlaceOnTop(topBox, x));
			foreach(var box in compatibleBoxes)
			{
				boxes.Remove(box);
				int currentValue = maxStackHeight(boxes, box);
				boxes.Add(box);
				if (currentValue > value)
					value = currentValue;
			}

			return value;
		}
		private static bool canPlaceOnTop(Box baseBox, Box top)
		{
			return 
				baseBox == null || 
					(baseBox.Depth > top.Depth && 
					baseBox.Heigh > top.Heigh &&
					baseBox.Width > top.Width);
		}

		public static int BooleanEvalutation(string s, bool result)
		{
			if (s.Length == 0)
				return 0;
			else if (s.Length == 1)
				return result ? (s == "0" ? 0 : 1) : (s == "1" ? 0 : 1);

			int count = 0;
			for(int i = 0; i < s.Length; i++)
			{
				char c = s[i];

				if (c == '|' || c == '&' || c == '^')
				{
					string left = s.Substring(0, i);
					string right = s.Substring(i + 1);

					int leftTrue = BooleanEvalutation(left, true);
					int leftFalse = BooleanEvalutation(left, false);
					int rightTrue = BooleanEvalutation(right, true);
					int rightFalse = BooleanEvalutation(right, false);

					if (c == '|')
						count += result ? (leftTrue * rightFalse) + (leftFalse * rightTrue) + (leftTrue * rightTrue) : (leftFalse * rightFalse);
					else if (c == '&')
						count += result ? (leftTrue * rightTrue) : (leftTrue * rightFalse) + (leftFalse * rightTrue) + (leftFalse * rightFalse);
					else if (c == '^')
						count += result ? (leftTrue * rightFalse) + (leftFalse * rightTrue) : (leftFalse * rightFalse) + (leftTrue * rightTrue);
				}
			}
			return count;


		}
	}
}
