namespace Phantom.StatSystem
{
	[System.Serializable]
	public struct StatValue
	{
		public StatType type;

		public float value;

		public float GetValue(StatSheet statSheet)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			return statSheet.GetValue(type) * value;
		}

		public void Apply(StatSheet statSheet)
		{
			if (statSheet == null)
				throw new System.ArgumentNullException("statSheet");

			statSheet.GetStat(type).BaseValue += value;
		}
	}
}
