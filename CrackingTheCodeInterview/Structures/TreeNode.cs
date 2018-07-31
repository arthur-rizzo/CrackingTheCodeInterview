using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview.Structures
{
    public class TreeNode
    {
		public TreeNode(int value)
		{
			Value = value;
		}
		public int Value { get; set; }

		private TreeNode left;
		public TreeNode Left
		{
			get
			{
				return left;
			}
			set
			{
				if(left != null)
					left.Parent = null;

				left = value;

				if(value != null)
					left.Parent = this;
			}
		}

		private TreeNode right;
		public TreeNode Right
		{
			get
			{
				return right;
			}
			set
			{
				if (right != null)
					right.Parent = null;

				right = value;

				if (value != null)
					right.Parent = this;
			}
		}

		public TreeNode Parent { get; set; }
	}
}
