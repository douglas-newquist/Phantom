using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Phantom
{
	[DisallowMultipleComponent]
	public class StatSheet : MonoBehaviour
	{
		public Entity Entity => GetComponent<Entity>();

		[SerializeField]
		private Dictionary<StatSO, Stat> stats = new Dictionary<StatSO, Stat>();

		private List<IStatusEffect> statusEffects = new List<IStatusEffect>();

		public IEnumerable<IStatusEffect> StatusEffects => statusEffects;

		[SerializeField]
		private ResourceStatSO primaryHealthStat;

		public ResourceStatSO PrimaryHealthStat => primaryHealthStat;

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
			foreach (var resource in GetStatsOfType<ResourceStat>())
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

		public bool HasStat(StatSO type) => stats.ContainsKey(type);

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
