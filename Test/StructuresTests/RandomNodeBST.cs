using CrackingTheCodeInterview.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Test.StructuresTests
{
	[TestClass]
    public class RandomNodeBST
    {
		[TestMethod]
		public void RandomNode()
		{
			RandomTree t = new RandomTree();
			t.Insert(10);
			t.Insert(6);
			t.Insert(15);
			t.Insert(14);
			t.Insert(22);
			t.Insert(1);
			t.Insert(4);
			t.Insert(7);
			t.Insert(13);
			t.Insert(40);

			Dictionary<int, int> occurrences = new Dictionary<int, int>();

			int x = 0;
			do
			{
				var node = t.RandomNode();
				if (!occurrences.ContainsKey(node.Data))
					occurrences[node.Data] = 1;
				else
					occurrences[node.Data]++;

				x++;
			} while (x < 100000);

			foreach(var pair in occurrences)
			{
				Trace.WriteLine($"Valor {pair.Key}: {((decimal)pair.Value / (decimal)100000) * 100}%");
			}
		}
    }
}
