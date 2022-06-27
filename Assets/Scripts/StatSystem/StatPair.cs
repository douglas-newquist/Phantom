namespace Phantom.StatSystem
{
	[System.Serializable]
	public struct StatPair
	{
		public StatType stat;

		public float baseValue;

		public float GetValue(StatSheet statSheet)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			return statSheet.GetValue(stat) * baseValue;
		}

		public void Apply(StatSheet statSheet)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			statSheet.GetStat(stat).BaseValue += baseValue;
		}
	}
}
