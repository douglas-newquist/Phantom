using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	[DisallowMultipleComponent]
	public class Entity : MonoBehaviour
	{
		public StatSheet Stats => GetComponent<StatSheet>();

		public ResourceStatSO primaryHealthStat;

		public UnityEvent<DamagedEvent> OnTakeDamage;

		public UnityEvent<DamagedEvent> OnTakeFatalDamage;

		public UnityEvent<Entity> OnKilled;

		public virtual void TakeDamage(Damage damage)
		{
			OnTakeDamage.Invoke(new DamagedEvent(this, damage));

			damage.Apply(Stats);

			var resource = Stats.GetStat<ResourceStat>(primaryHealthStat);

			if (resource.Current == 0)
				OnTakeFatalDamage.Invoke(new DamagedEvent(this, damage));

			if (resource.Current == 0)
			{
				OnDeath();
				OnKilled.Invoke(this);
			}
		}

		public virtual void OnDeath()
		{
			Destroy(gameObject);
		}
	}
	public class Ship : Entity
	{

	}
	public class Projectile : MonoBehaviour
	{

	}
	public class Homing : MonoBehaviour
	{

	}
}
