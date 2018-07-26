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

		public TreeNode Left { get; set; }

		public TreeNode Right { get; set; }
	}
}
