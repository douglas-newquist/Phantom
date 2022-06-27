namespace Phantom
{
	/// <summary>
	/// Defines a modifier, its strength and what stat it applies to
	/// </summary>
	[System.Serializable]
	public struct Modifier
	{
		public StatType stat;

		public ModifierSO modifier;

		public float magnitude;

		public IModifier Create(object source = null)
		{
			if (modifier == null)
				throw new System.ArgumentNullException("Modifier is not selected");

			return modifier.Create(source, magnitude);
		}

		public IModifier Apply(StatSheet statSheet, object source = null)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			var modifier = Create(source);

			statSheet.GetStat<IModifiableStat>(stat).AddModifier(modifier);
			return modifier;
		}
	}
}
