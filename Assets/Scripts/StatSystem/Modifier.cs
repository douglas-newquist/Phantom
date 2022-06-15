namespace Phantom
{
	/// <summary>
	/// Defines a modifier, its strength and what stat it applies to
	/// </summary>
	[System.Serializable]
	public struct Modifier
	{
		public StatSO stat;

		public ModifierSO modifier;

		public float magnitude;

		public IModifier Create(object source = null)
		{
			if (modifier == null)
				throw new System.ArgumentNullException("Modifier is not selected");

			return modifier.Create(source, magnitude);
		}

		public void Apply(StatSheet statSheet, object source = null)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			statSheet.GetStat(stat).AddModifier(Create(source));
		}
	}
}
