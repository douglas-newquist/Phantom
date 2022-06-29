using UnityEngine;

namespace Phantom.StatSystem
{
	/// <summary>
	/// Defines a specific stat with some value
	/// </summary>
	[System.Serializable]
	public struct StatValue
	{
		[SerializeField]
		private StatType type;

		public StatType Type
		{
			get => type;
			set => type = value;
		}

		[SerializeField]
		private float value;

		public float Value
		{
			get => value;
			set => this.value = value;
		}

		public float GetValue(StatSheet statSheet)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			return statSheet.GetValue(Type) * Value;
		}

		public void Apply(StatSheet statSheet)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			statSheet.GetStat(Type).BaseValue += Value;
		}
	}
}
