using System.Collections.Generic;

namespace Phantom.StatSystem
{
	[System.Serializable]
	public class StatSheetDefaults
	{
		public List<StatValue> stats = new List<StatValue>();

		public List<Modifier> modifiers = new List<Modifier>();

		public void Apply(StatSheet statSheet)
		{
			foreach (var stat in stats)
				stat.Apply(statSheet);

			foreach (var modifier in modifiers)
				modifier.Apply(statSheet);
		}
	}
}
