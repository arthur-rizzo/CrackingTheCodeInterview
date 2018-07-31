using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview.Structures
{
	public class RandomTree
	{
		public RandomNodeBST Root { get; set; }

		private Random r = new Random(23423423);

		public void Insert(int value)
		{
			if (Root == null)
				Root = new RandomNodeBST(value);
			else
				Root.Insert(value);
		}

		public bool Remove(int value)
		{
			if (Root == null)
				return false;

			if (Root.Data == value)
			{
				Root = null;
				return true;
			}
			else
				return Root.RemoveChild(value);

		}

		public RandomNodeBST Find(int value)
		{
			return Root?.Find(value);
		}

		public RandomNodeBST RandomNode()
		{
			if (Root == null)
				return null;

			return Root.RandomNode(r.Next(1, Root.Size));
		}
	}

    public class RandomNodeBST
    {
		public int Size { get; private set; } = 1;

		public RandomNodeBST(int value)
		{
			Data = value;
		}

		public int Data { get; set; }

		public RandomNodeBST Left { get; set; }

		public RandomNodeBST Right { get; set; }

		public void Insert(int value)
		{
			if(value <= Data)
			{
				if (Left != null)
					Left.Insert(value);
				else
					Left = new RandomNodeBST(value);
			}
			else
			{
				if (Right != null)
					Right.Insert(value);
				else
					Right = new RandomNodeBST(value);
			}

			updateSize();
		}

		public RandomNodeBST Find(int value)
		{
			if (value == Data)
				return this;
			else if (value < Data)
				return Left?.Find(value);
			else
				return Right?.Find(value);
		}

		public bool RemoveChild(int value)
		{
			bool result = false;
			if (value < Data)
			{
				if (Left != null)
				{
					if (Left.Data == value)
					{
						Left = null;
						result = true;
					}
					else
						result = Left.RemoveChild(value);
				}
				else
					result = false;
			}
			else
			{
				if (Right != null)
				{
					if (Right.Data == value)
					{
						Right = null;
						result = true;
					}
					else
						result = Right.RemoveChild(value);
				}
				else
					result = false;
			}

			updateSize();
			return result;
		}

		public RandomNodeBST RandomNode(int value)
		{
			int leftSize = Left?.Size ?? 0;

			if (value <= leftSize)
				return Left.RandomNode(value);
			else if (value == (leftSize + 1))
				return this;
			else
				return Right.RandomNode(value - (leftSize + 1));
		}

		private void updateSize()
		{
			Size = 1 + ((Left?.Size) ?? 0) + ((Right?.Size) ?? 0);
		}
	}
}
