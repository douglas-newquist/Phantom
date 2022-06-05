namespace Game
{
	[System.Serializable]
	public class StatList
	{
		public StatPair[] stats;

		public void PopulateStatSheet(StatSheet sheet)
		{
			foreach (var pair in stats)
			{
				var stat = sheet.AddStat(pair.stat, pair.stat.Create());
				stat.BaseValue = pair.baseValue;
			}
		}
	}
}
