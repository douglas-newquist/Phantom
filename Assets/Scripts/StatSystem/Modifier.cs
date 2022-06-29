using UnityEngine;

namespace Phantom.StatSystem
{
	/// <summary>
	/// Defines a modifier, its strength and what stat it applies to
	/// </summary>
	[System.Serializable]
	public struct Modifier
	{
		[SerializeField]
		private StatType stat;

		public StatType StatType { get => stat; set => stat = value; }

		[SerializeField]
		private ModifierType modifier;

		public ModifierType ModifierType { get => modifier; set => modifier = value; }

		[SerializeField]
		private float magnitude;

		public float Magnitude { get => magnitude; set => magnitude = value; }

		/// <summary>
		/// Creates a new runtime version of this modifier
		/// </summary>
		/// <param name="source">What is giving this modifier</param>
		public IModifier Create(object source = null)
		{
			if (ModifierType == null)
				throw new System.ArgumentNullException("Modifier is not selected");

			return ModifierType.Create(source, Magnitude);
		}

		public IModifier Apply(StatSheet statSheet, object source = null)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			var modifier = Create(source);

			statSheet.GetStat<IModifiableStat>(StatType).AddModifier(modifier);
			return modifier;
		}
	}
}
