using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(Entity))]
	[DisallowMultipleComponent]
	public class StatSheet : MonoBehaviour
	{
		public Entity Entity => GetComponent<Entity>();

		[SerializeField]
		private Dictionary<StatSO, Stat> stats = new Dictionary<StatSO, Stat>();

		public override string ToString()
		{
			string s = "Stat Sheet " + stats.Count + " stats";

			foreach (var stat in stats.Values)
				s += "\n" + stat.ToString();

			return s;
		}

		/// <summary>
		/// Adds a stat to this stat sheet, rejects if there is already a stat of the same type.
		/// </summary>
		/// <typeparam name="T">The stat class type</typeparam>
		/// <param name="type">The stat type</param>
		/// <param name="stat">The stat to add</param>
		/// <returns>The stat</returns>
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
			stat.Sheet = this;
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

		public float GetValue(StatSO type) => GetStat(type).Value;

		/// <summary>
		/// Gets all stats of a given type
		/// </summary>
		/// <typeparam name="T">Stat class type to filter</typeparam>
		public IEnumerable<T> GetStatsOfType<T>() where T : Stat
		{
			foreach (var stat in stats.Values)
				if (stat is T)
					yield return stat as T;
		}

		/// <summary>
		/// Removes all modifiers from each stat from the given source
		/// </summary>
		public void RemoveAllModifiersFromSource(object source)
		{
			foreach (var stat in stats.Values)
				stat.RemoveModifiersFromSource(source);
		}
	}
}
