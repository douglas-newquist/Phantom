using UnityEngine;
using UnityEngine.Events;

namespace Phantom
{
	public class Projectile : MonoBehaviour
	{
		public StatSheet statSheet;

		public ProjectileSO ProjectileStats;

		public Damage damage;

		public float DeathTime { get; set; }

		public UnityEvent<Projectile> OnExpired;

		public UnityEvent<ProjectileHitEvent> OnHit;

		protected virtual void Start()
		{

		}

		public virtual void Update()
		{
			if (Time.time > DeathTime)
			{
				OnExpired.Invoke(this);
				ObjectPool.Despawn(gameObject);
			}
		}
	}
}
