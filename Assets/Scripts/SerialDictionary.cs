using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Phantom
{
	/// <summary>
	/// Dictionary that supports Unity serialization
	/// </summary>
	[System.Serializable]
	public class SerialDictionary<TKey, TValue> : ISerializationCallbackReceiver, IDictionary<TKey, TValue>
	{
		[SerializeField]
		private List<TKey> keys = new List<TKey>();

		[SerializeField]
		private List<TValue> values = new List<TValue>();

		[System.NonSerialized]
		private IDictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

		public TValue this[TKey key]
		{
			get => dict[key];
			set => dict[key] = value;
		}

		public ICollection<TKey> Keys => dict.Keys;

		public ICollection<TValue> Values => dict.Values;

		public int Count => dict.Count;

		public bool IsReadOnly => false;

		public void Add(TKey key, TValue value)
		{
			dict.Add(key, value);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			dict.Add(item);
		}

		public void Clear()
		{
			dict.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return dict.Contains(item);
		}

		public bool ContainsKey(TKey key)
		{
			return dict.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			dict.CopyTo(array, arrayIndex);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dict.GetEnumerator();
		}

		public void OnAfterDeserialize()
		{
			dict = new Dictionary<TKey, TValue>();
			for (int i = 0; i < keys.Count && i < values.Count; i++)
				dict[keys[i]] = values[i];
		}

		public void OnBeforeSerialize()
		{
			keys = new List<TKey>(dict.Keys);
			values = new List<TValue>(dict.Values);
		}

		public bool Remove(TKey key)
		{
			return dict.Remove(key);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return dict.Remove(item);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return dict.TryGetValue(key, out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return dict.GetEnumerator();
		}
	}
}
