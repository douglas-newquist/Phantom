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

		public GameObject Owner { get; set; }

		public UnityEvent<GameObject> OnExpired;

		public UnityEvent<GameObject> OnHit;

		private void Start()
		{
			OnSpawn();
		}

		protected virtual void OnSpawn()
		{
			if (TryGetComponent<TrailRenderer>(out var trail))
				trail.Clear();
		}

		public virtual void Update()
		{
			if (Time.time > DeathTime)
			{
				OnExpired.Invoke(gameObject);
				ObjectPool.Despawn(gameObject);
			}
		}
	}
}
