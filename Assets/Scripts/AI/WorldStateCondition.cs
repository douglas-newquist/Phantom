using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class WorldStateCondition
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
		private Relation comparison;

		public Relation Comparison
		{
			get => comparison;
			set => comparison = value;
		}

		[SerializeField]
		private int value;

		public int Value
		{
			get => value;
			set => this.value = value;
		}

		public WorldStateCondition(string key, Relation comparison, int value)
		{
			Key = key;
			Comparison = comparison;
			Value = value;
		}

		public WorldStateCondition() { }

		public enum Relation
		{
			EqualTo,
			NotEqualTo,
			AtLeast,
			AtMost,
			HasState,
			DoesNotHaveState
		}

		public bool ValueMatches(int value)
		{
			switch (Comparison)
			{
				case Relation.EqualTo: return value == Value;
				case Relation.NotEqualTo: return value != Value;
				case Relation.AtLeast: return value >= Value;
				case Relation.AtMost: return value <= Value;
				case Relation.HasState: return true;
				case Relation.DoesNotHaveState: return false;
				default: return false;
			}
		}

		public bool Satisfied(WorldStates worldStates)
		{
			if (worldStates.TryGetState(Key, out var state))
				return ValueMatches(state.Value);

			switch (Comparison)
			{
				case Relation.DoesNotHaveState: return true;
			}

			return false;
		}
	}
}
