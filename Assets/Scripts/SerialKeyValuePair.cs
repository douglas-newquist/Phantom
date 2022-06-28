using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public struct SerialKeyValuePair<TKey, TValue>
	{
		[SerializeField]
		private TKey key;

		public TKey Key
		{
			get => key;
			set => key = value;
		}

		[SerializeField]
		private TValue value;

		public TValue Value
		{
			get => value;
			set => this.value = value;
		}

		public SerialKeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		public static implicit operator KeyValuePair<TKey, TValue>(SerialKeyValuePair<TKey, TValue> item)
		{
			return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
		}

		public static implicit operator SerialKeyValuePair<TKey, TValue>(KeyValuePair<TKey, TValue> item)
		{
			return new SerialKeyValuePair<TKey, TValue>(item.Key, item.Value);
		}
	}
}
