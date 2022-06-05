namespace Game
{
	[System.Serializable]
	public struct StatPair
	{
		public StatSO stat;

		public float baseValue;

		public float GetValue(StatSheet statSheet)
		{
			return statSheet.GetValue(stat) * baseValue;
		}
	}
}
