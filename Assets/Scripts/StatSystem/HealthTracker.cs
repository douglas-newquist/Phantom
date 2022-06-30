using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Phantom.StatSystem
{
	[RequireComponent(typeof(StatSheet))]
	[DisallowMultipleComponent]
	public class HealthTracker : MonoBehaviour, IDamageable
	{
		private StatSheet stats;

		public StatSheet Stats => stats;

		[SerializeField]
		private bool invulnerable = false;

		/// <summary>
		/// Is this entity immune to all forms of damage
		/// </summary>
		public bool Invulnerable
		{
			get => invulnerable;
			set => invulnerable = value;
		}

		[SerializeField]
		private ResourceStatType[] healthStatTypes;

		public bool IsAlive
		{
			get
			{
				if (healthStatTypes.Length == 0) return true;
				return GetResource(healthStatTypes[0]).Empty == false;
			}
		}

		public bool IsDead => !IsAlive;

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
			stats = GetComponent<StatSheet>();
		}

		private ResourceStat GetResource(ResourceStatType type)
		{
			return Stats.GetStat<ResourceStat>(type);
		}

		public void ApplyDamage(Damage damage)
		{
			if (Invulnerable || IsDead || damage.Amount == 0) return;

			var damageEvent = new DamagedEvent(Stats, damage);
			OnTakeDamage.Invoke(damageEvent);
			damage = damageEvent.Damage;

			damage.Apply(Stats);

			if (IsDead)
			{
				damageEvent = new DamagedEvent(Stats, damage);
				OnTakeFatalDamage.Invoke(damageEvent);
				damage = damageEvent.Damage;
			}

			if (IsDead)
				OnDeath.Invoke(Stats);
		}
	}
}
