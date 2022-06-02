using UnityEngine;

namespace Game
{
	public class Projectile : MonoBehaviour
	{
		public StatSheet statSheet;

		public ProjectileSO ProjectileStats;

		public Damage damage;

		public float DeathTime { get; set; }

		protected virtual void Start()
		{
			DeathTime = Time.time + ProjectileStats.GetLifeSpan(statSheet);
		}

		public virtual void Update()
		{
			if (Time.time > DeathTime)
				ObjectPool.Despawn(gameObject);
		}
	}
}
