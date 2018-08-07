using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodeInterview
{
    public class Sorting
    {
		public static void BubbleSort(int[] array)
		{
			bool sorted = true;
			do
			{
				bool swaped = false;
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (array[i] > array[i + 1])
					{
						int temp = array[i + 1];
						array[i + 1] = array[i];
						array[i] = temp;
						swaped = true;
					}
				}

				sorted = !swaped;

			} while (!sorted);
		}

		public static void MergeSort(int[] array)
		{
			mergeSort(array, 0, array.Length - 1, new int[array.Length]);
		}

		private static void mergeSort(int[] array, int low, int high, int[] helper)
		{
			if(low < high)
			{ 
				int midPoint = (low + high) / 2;
				mergeSort(array, low, midPoint, helper);
				mergeSort(array, midPoint + 1, high, helper);
				merge(array, low, high, midPoint, helper);
			}
		}

		private static void merge(int[] array, int low, int high, int midpoint, int[] helper)
		{
			for (int i = low; i <= high; i++)
				helper[i] = array[i];

			int helperLeft = low;
			int helperRight = midpoint + 1;
			int current = low;

			while(helperLeft <= midpoint && helperRight <= high)
			{
				if(helper[helperLeft] < helper[helperRight])
				{
					array[current] = helper[helperLeft];
					helperLeft++;
				}
				else
				{
					array[current] = helper[helperRight];
					helperRight++;
				}

				current++;
			}

			//copia o resto do left (se houver resto do right, ele ja esta no lugar)
			while(helperLeft <= midpoint)
			{
				array[current] = helper[helperLeft];
				helperLeft++;
				current++;
			}

		}

		public static void QuickSort(int[] array)
		{
			quickSort(array, 0, array.Length - 1);
		}

		private static void quickSort(int[] array, int start, int end)
		{
			if(start < end)
			{ 
				int pivotEndIndex = partition(array, start, end);
				quickSort(array, start, pivotEndIndex - 1);
				quickSort(array, pivotEndIndex + 1, end);
			}
		}

		private static int partition(int[] array, int start, int end)
		{
			int pivot = array[(start + end) / 2];
			int i = start;
			int j = end;

			while(i <= j)
			{
				while (array[i] < pivot)
					i++;

				while (array[j] > pivot)
					j--;

				if(i <= j)
				{
					swap(array, i, j);
					i++;
					j--;
				}
			}

			return i;
		}

		private static void swap(int[] array, int a, int b)
		{
			int temp = array[a];
			array[a] = array[b];
			array[b] = temp;
		}

	}
}
