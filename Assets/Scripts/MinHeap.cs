using System.Collections.Generic;
using System;

namespace Phantom
{
	[System.Serializable]
	public class MinHeap<TValue, TPriority> : IHeap<TValue, TPriority> where TPriority : IComparable<TPriority>
	{
		[System.Serializable]
		class HeapElement
		{
			public TValue element;

			public TPriority priority;

			public HeapElement(TValue element, TPriority priority)
			{
				this.element = element;
				this.priority = priority;
			}
		}

		List<HeapElement> elements = new List<HeapElement>();

		public bool Empty => elements.Count == 0;

		public void Insert(TValue element, TPriority priority)
		{
			int i = elements.Count;
			int parent = i / 2;
			elements.Add(new HeapElement(element, priority));

			while (elements[i].priority.CompareTo(elements[parent].priority) < 0)
			{
				var tmp = elements[i];
				elements[i] = elements[parent];
				elements[parent] = tmp;

				i = parent;
				parent = i / 2;
			}
		}

		public TValue Peek()
		{
			return elements[0].element;
		}

		public TValue Extract()
		{
			var element = elements[0];
			elements[0] = elements[elements.Count - 1];
			elements.RemoveAt(elements.Count - 1);
			Heapify(0);

			return element.element;
		}

		public bool TryExtract(out TValue element)
		{
			if (Empty)
			{
				element = default(TValue);
				return false;
			}

			element = Extract();
			return true;
		}

		void Heapify(int i)
		{
			int left = i * 2 + 1;
			int right = left + 1;
			int smallest = i;

			if (left < elements.Count && elements[left].priority.CompareTo(elements[smallest].priority) < 0)
				smallest = left;

			if (right < elements.Count && elements[right].priority.CompareTo(elements[smallest].priority) < 0)
				smallest = right;

			if (smallest != i)
			{
				var tmp = elements[i];
				elements[i] = elements[smallest];
				elements[smallest] = tmp;

				Heapify(smallest);
			}
		}
	}
}
