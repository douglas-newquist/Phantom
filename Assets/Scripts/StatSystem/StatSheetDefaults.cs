using System.Collections.Generic;
using UnityEngine;

namespace Phantom.StatSystem
{
	[System.Serializable]
	public class StatSheetDefaults
	{
		[SerializeField]
		private List<StatValue> stats = new List<StatValue>();

		[SerializeField]
		private List<Modifier> modifiers = new List<Modifier>();

		public void Apply(StatSheet statSheet)
		{
			foreach (var stat in stats)
				stat.Apply(statSheet);

			foreach (var modifier in modifiers)
				modifier.Apply(statSheet);
		}
	}
}
