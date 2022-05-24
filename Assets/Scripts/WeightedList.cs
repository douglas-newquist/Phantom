using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class WeightedList<T> : IList<WeightedItem<T>>
	{
		[SerializeField]
		private List<WeightedItem<T>> items = new List<WeightedItem<T>>();

		public WeightedItem<T> this[int index] {
			get => items[index];
			set => items[index] = value; }

		public float TotalWeight
		{
			get
			{
				float total = 0;

				foreach (var item in items)
					total += item.weight;

				return total;
			}
		}

		public int Count => items.Count;

		public bool IsReadOnly => false;

		public void Add(WeightedItem<T> item)
		{
			items.Add(item);
		}

		public void Clear()
		{
			items.Clear();
		}

		public bool Contains(WeightedItem<T> item)
		{
			return items.Contains(item);
		}

		public void CopyTo(WeightedItem<T>[] array, int arrayIndex)
		{
			items.CopyTo(array, arrayIndex);
		}

		public IEnumerator<WeightedItem<T>> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public T GetRandom()
		{
			float total = 0;
			float roll = Random.Range(0, TotalWeight);

			foreach (var item in items)
			{
				total += item.weight;
				if (total >= roll)
					return item.value;
			}

			return default(T);
		}

		public int IndexOf(WeightedItem<T> item)
		{
			return items.IndexOf(item);
		}

		public void Insert(int index, WeightedItem<T> item)
		{
			items.Insert(index, item);
		}

		public bool Remove(WeightedItem<T> item)
		{
			return items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			items.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}
	}
}
