using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public struct WorldState
	{
		[SerializeField]
		private string key;

		public string Key
		{
			get => key;
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new System.ArgumentNullException("Key");
				key = value;
			}
		}

		[SerializeField]
		private int value;

		public int Value
		{
			get => value;
			set => this.value = value;
		}

		public bool BoolValue
		{
			get => Value != 0;
			set => Value = value ? 1 : 0;
		}

		public WorldState(string key, int value) : this()
		{
			Key = key;
			Value = value;
		}

		public WorldState(string key, bool value) : this()
		{
			Key = key;
			BoolValue = value;
		}

		public override string ToString()
		{
			return "State '" + Key + "' with value " + Value;
		}

		public override int GetHashCode()
		{
			return Key.GetHashCode();
		}
	}
}
