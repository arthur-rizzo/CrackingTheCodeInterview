using CrackingTheCodeInterview.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.StructuresTests
{
	[TestClass]
	public class AnimalShelterTests
	{
		[TestMethod]
		public void AnimalShelterTest()
		{
			AnimalShelter s = new AnimalShelter();

			s.Enqueue(newDog("toto"));
			s.Enqueue(newDog("barney"));
			s.Enqueue(newCat("boris"));
			s.Enqueue(newCat("hans"));

			var a = s.DequeueAny();
			Assert.AreEqual(a.Name, "toto");

			a = s.DequeueCat();
			Assert.AreEqual(a.Name, "boris");

			a = s.DequeueAny();
			Assert.AreEqual(a.Name, "barney");

			Assert.ThrowsException<InvalidOperationException>(() => s.DequeueDog());

			a = s.DequeueAny();
			Assert.AreEqual(a.Name, "hans");

			Assert.ThrowsException<InvalidOperationException>(() => s.DequeueAny());
		}

		private Animal newDog(string name)
		{
			return new Animal() { Name = name, Species = Animals.Dog };
		}

		private Animal newCat(string name)
		{
			return new Animal() { Name = name, Species = Animals.Cat };
		}
	}
}
