using UnityEngine;
using UnityEngine.Events;
using Phantom.StatSystem;
using Phantom.ObjectPooling;

namespace Phantom
{
	public class Projectile : MonoBehaviour, IProjectile
	{
		public Damage Damage { get; set; }

		public float Acceleration { get; set; }

		public float DeathTime { get; set; }

		public UnityEvent<GameObject> OnExpired;

		public UnityEvent<GameObject> OnHit;


		public virtual void Update()
		{
			if (Time.time > DeathTime)
			{
				OnExpired.Invoke(gameObject);
				ObjectPool.Despawn(gameObject);
			}
		}

		public void Fire(GameObject owner, float lifeSpan)
		{
			throw new System.NotImplementedException();
		}
	}
}
