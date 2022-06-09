using System.Collections.Generic;
using System;

namespace Phantom
{
	[System.Serializable]
	public class MinHeap<T> : IHeap<T> where T : IComparable<T>
	{
		List<T> elements = new List<T>();

		public int Count => elements.Count;

		public bool Empty => elements.Count == 0;

		protected int Parent(int i) => (i - 1) / 2;

		protected int LeftChild(int i) => i * 2 + 1;

		protected int RightChild(int i) => i * 2 + 2;

		protected bool IsLeaf(int i) => i >= elements.Count / 2;

		/// <summary>
		/// Inserts a new element into this heap
		/// </summary>
		/// <param name="value">Value to insert</param>
		public void Insert(T value)
		{
			int i = Count;
			elements.Add(value);

			while (i > 0 && elements[i].CompareTo(elements[Parent(i)]) < 0)
			{
				Utilities.Swap(elements, i, Parent(i));
				i = Parent(i);
			}
		}

		/// <summary>
		/// Gets the current minimum value without removing it
		/// </summary>
		public T Peek() => elements[0];

		/// <summary>
		/// Gets the current minimum value without removing it
		/// </summary>
		/// <param name="value">The minimum value</param>
		/// <returns>True if the value is valid</returns>
		public bool TryPeek(out T value)
		{
			if (Empty)
			{
				value = default(T);
				return false;
			}

			value = Peek();
			return true;
		}

		/// <summary>
		/// Pops the minimum value off this heap
		/// </summary>
		public T Extract()
		{
			for (int i = Count - 1; i >= 0; i--)
				MinHeapify(i);
			var value = elements[0];

			if (Count == 1)
				elements.Clear();
			else
			{
				Utilities.Swap(elements, 0, elements.Count - 1);
				elements.RemoveAt(elements.Count - 1);

				if (Count > 1)
					MinHeapify(0);
			}

			return value;
		}

		public bool TryExtract(out T element)
		{
			if (Empty)
			{
				element = default(T);
				return false;
			}

			element = Extract();
			return true;
		}

		protected void MinHeapify(int i)
		{
			int left = LeftChild(i);
			int right = RightChild(i);
			int smallest = i;

			if (left < Count)
				if (elements[left].CompareTo(elements[smallest]) < 0)
					smallest = left;

			if (right < elements.Count)
				if (elements[right].CompareTo(elements[smallest]) < 0)
					smallest = right;

			if (smallest != i)
			{
				Utilities.Swap(elements, smallest, i);
				MinHeapify(smallest);
			}
		}

		public void Clear() => elements.Clear();
	}
}
