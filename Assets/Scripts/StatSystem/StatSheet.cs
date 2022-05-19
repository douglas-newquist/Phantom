using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class StatSheet
	{
		private Dictionary<StatSO, Stat> stats = new Dictionary<StatSO, Stat>();

		public T AddStat<T>(StatSO type, T stat) where T : Stat
		{
			if (type == null)
			{
				Debug.LogError("Null stat type");
				return null;
			}

			if (stats.ContainsKey(type))
				return stats[type] as T;

			stats[type] = stat;
			return stat;
		}

		public Stat GetStat(StatSO type)
		{
			if (type == null)
			{
				Debug.LogError("Null stat type");
				return null;
			}

			if (stats.TryGetValue(type, out Stat stat))
				return stat;

			return AddStat(type, type.Create());
		}

		public T GetStat<T>(StatSO type) where T : Stat
		{
			return GetStat(type) as T;
		}

		public void RemoveAllModifiersFromSource(object source)
		{
			foreach (var stat in stats)
				stat.Value.RemoveModifiersFromSource(source);
		}
	}
}
