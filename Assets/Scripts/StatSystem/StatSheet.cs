using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Phantom.StatSystem
{
	[DisallowMultipleComponent]
	public class StatSheet : MonoBehaviour, IDamageable
	{
		[SerializeField]
		private Dictionary<StatType, IStat> stats = new Dictionary<StatType, IStat>();

		[SerializeField]
		private List<IStatusEffect> statusEffects = new List<IStatusEffect>();

		public IEnumerable<IStatusEffect> StatusEffects => statusEffects;

		[SerializeField]
		private ResourceStatType primaryHealthStat;

		public ResourceStatType PrimaryHealthStat => primaryHealthStat;

		/// <summary>
		/// Triggers whenever this entity takes damage
		/// </summary>
		public UnityEvent<DamagedEvent> OnTakeDamage;

		/// <summary>
		/// Triggers after taking an amount of damage that will kill this entity
		/// </summary>
		public UnityEvent<DamagedEvent> OnTakeFatalDamage;

		/// <summary>
		/// Triggers after OnTakeFatalDamage if this entity is still dying
		/// </summary>
		public UnityEvent<StatSheet> OnDeath;

		private void Start()
		{
			foreach (var resource in GetStatsOfType<IResourceStat>())
				resource.Reset();
		}

		public virtual void ApplyDamage(Damage damage)
		{
			if (damage.amount == 0) return;

			var damageEvent = new DamagedEvent(this, damage);
			OnTakeDamage.Invoke(damageEvent);
			damage = damageEvent.Damage;

			damage.Apply(this);

			var resource = GetStat<ResourceStat>(PrimaryHealthStat);

			if (resource.Empty)
			{
				damageEvent = new DamagedEvent(this, damage);
				OnTakeFatalDamage.Invoke(damageEvent);
				damage = damageEvent.Damage;
			}

			if (resource.Empty)
				OnDeath.Invoke(this);
		}

		public override string ToString()
		{
			string s = "Stat Sheet with " + stats.Count + " stats";

			foreach (var stat in stats.Values)
				s += "\n" + stat.ToString();

			return s;
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
			{
				Debug.LogError("Null stat type");
				return default(T);
			}

			if (stats.ContainsKey(type))
				return (T)stats[type];

			stats[type] = stat;
			stat.Sheet = this;
			return stat;
		}

		public IStat GetStat(StatType type)
		{
			if (type == null)
			{
				Debug.LogError("Null stat type");
				return null;
			}

			if (stats.TryGetValue(type, out IStat stat))
				return stat;

			return AddStat(type, type.Create());
		}

		public T GetStat<T>(StatType type) where T : IStat
		{
			IStat stat = GetStat(type);
			if (stat is T) return (T)stat;
			return default(T);
		}

		public float GetValue(StatType type) => GetStat(type).Value;

		/// <summary>
		/// Gets all stats of a given type
		/// </summary>
		/// <typeparam name="T">Stat class type to filter</typeparam>
		public IEnumerable<T> GetStatsOfType<T>() where T : IStat
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
	}
}
