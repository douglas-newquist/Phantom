using UnityEngine;
using UnityEngine.Events;
using Phantom.StatSystem;

namespace Phantom
{
	public interface IProjectile
	{
		float DeathTime { get; set; }

		void Update();
	}

	public class Projectile : MonoBehaviour, IProjectile
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
