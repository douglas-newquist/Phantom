using System.Collections.Generic;

namespace Phantom
{
	[System.Serializable]
	public class StatList
	{
		public List<StatPair> stats = new List<StatPair>();

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
