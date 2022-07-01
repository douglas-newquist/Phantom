using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Phantom.StatSystem
{
	[DisallowMultipleComponent]
	public class StatSheet : MonoBehaviour, IEnumerable<IStat>, IReset
	{
		[SerializeField]
		private StatSheetDefaults statSheetDefaults;

		[SerializeField]
		private Dictionary<StatType, IStat> stats = new Dictionary<StatType, IStat>();

		public int StatCount => stats.Count;

		[SerializeField]
		private List<IStatusEffect> statusEffects = new List<IStatusEffect>();

		public IEnumerable<IStatusEffect> StatusEffects => statusEffects;

		[SerializeField]
		private bool autoCreateMissingStats = true;

		public bool AutoCreateMissingStats
		{
			get => autoCreateMissingStats;
			set => autoCreateMissingStats = value;
		}

		/// <summary>
		/// Triggers whenever this entity takes damage
		/// </summary>
		[Header("Events")]
		public UnityEvent<IStat> OnStatAdded;

		public UnityEvent<IStatusEffect> OnStatusEffectApplied;

		public UnityEvent<IStatusEffect> OnStatusEffectExpired;

		private void Start()
		{
			statSheetDefaults.Apply(this);
			Reset();
		}

		public bool HasStat(StatType type) => stats.ContainsKey(type);

		/// <summary>
		/// Adds a stat to this stat sheet, rejects if there is already a stat of the same type.
		/// </summary>
		/// <typeparam name="T">The stat class type</typeparam>
		/// <param name="type">The stat type</param>
		/// <param name="stat">The stat to add</param>
		/// <returns>The stat</returns>
		public T AddStat<T>(StatType type, T stat) where T : IStat
		{
			if (type == null)
				throw new System.ArgumentNullException("type");

			if (stats.ContainsKey(type))
				return (T)stats[type];

			stats[type] = stat;
			stat.Sheet = this;
			OnStatAdded.Invoke(stat);
			return stat;
		}

		public IStat GetStat(StatType type)
		{
			if (type == null)
				throw new System.ArgumentNullException("type");

			if (stats.TryGetValue(type, out IStat stat))
				return stat;

			if (AutoCreateMissingStats)
				return AddStat(type, type.Create());

			throw new KeyNotFoundException("type");
		}

		public T GetStat<T>(StatType type) where T : IStat
		{
			return (T)GetStat(type);
		}

		public bool TryGetStat(StatType type, out IStat stat)
		{
			return stats.TryGetValue(type, out stat);
		}

		public bool TryGetStat<T>(StatType type, out T stat) where T : IStat
		{
			if (TryGetStat(type, out var s) && s is T)
			{
				stat = (T)s;
				return true;
			}

			stat = default(T);
			return false;
		}

		public float GetValue(StatType type)
		{
			if (TryGetStat(type, out var stat))
				return stat.Value;
			return type.DefaultValue;
		}

		/// <summary>
		/// Gets all stats of a given type
		/// </summary>
		/// <typeparam name="T">Stat class type to filter</typeparam>
		public IEnumerable<T> GetStatsOfType<T>()
		{
			foreach (var stat in stats.Values)
				if (stat is T)
					yield return (T)stat;
		}

		/// <summary>
		/// Removes all modifiers from each stat from the given source
		/// </summary>
		public void RemoveAllModifiersFromSource(object source)
		{
			foreach (var stat in GetStatsOfType<IModifiableStat>())
				stat.RemoveModifiersFromSource(source);
		}

		public void AddModifier(Modifier modifier, object source)
		{
			modifier.Apply(this, source);
		}

		public void AddModifiers(IEnumerable<Modifier> modifiers, object source)
		{
			foreach (var mod in modifiers)
				mod.Apply(this, source);
		}

		public IEnumerator<IStat> GetEnumerator()
		{
			foreach (var stat in stats)
				yield return stat.Value;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (var stat in stats)
				yield return stat.Value;
		}

		/// <summary>
		/// Runs reset on all stats
		/// </summary>
		public void Reset()
		{
			foreach (var stat in GetStatsOfType<IReset>())
				stat.Reset();
		}

		/// <summary>
		/// Removes all stats and status effects
		/// </summary>
		public void Clear()
		{
			stats.Clear();
			statusEffects.Clear();
		}
	}
}
