using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview.Structures
{
	public enum Animals
	{
		Dog,
		Cat
	}

	public class Animal
	{
		public Animals Species { get; set; }
		public string Name { get; set; }
	}

    public class AnimalShelter
    {
		private class AnimalData
		{
			public DateTime AdmissionDate { get; set; }
			public Animal Animal { get; set; }
		}

		Queue<AnimalData> catQueue;
		Queue<AnimalData> dogQueue;

		public AnimalShelter()
		{
			catQueue = new Queue<AnimalData>();
			dogQueue = new Queue<AnimalData>();
		}

		public void Enqueue(Animal a)
		{
			AnimalData data = new AnimalData()
			{
				Animal = a,
				AdmissionDate = DateTime.Now
			};

			switch (a.Species)
			{
				case Animals.Dog:
					dogQueue.Enqueue(data);
					break;
				case Animals.Cat:
					catQueue.Enqueue(data);
					break;
				default:
					break;
			}
		}

		public Animal DequeueDog()
		{
			return dogQueue.Dequeue().Animal;
		}

		public Animal DequeueCat()
		{
			return catQueue.Dequeue().Animal;
		}

		public Animal DequeueAny()
		{
			if (catQueue.Count > 0 && dogQueue.Count > 0)
			{
				if(catQueue.Peek().AdmissionDate < dogQueue.Peek().AdmissionDate)
					return catQueue.Dequeue().Animal;
				else
					return dogQueue.Dequeue().Animal;
			}
			else if (catQueue.Count > 0)
			{
				return catQueue.Dequeue().Animal;
			}
			else if (dogQueue.Count > 0)
			{
				return dogQueue.Dequeue().Animal;
			}
			else
				throw new InvalidOperationException();
		}
	}

}
