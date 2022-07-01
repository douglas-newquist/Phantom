using UnityEngine;
using Phantom.StatSystem;
using Phantom.ObjectPooling;

namespace Phantom
{
	public abstract class ProjectileType : ScriptableObject
	{
		public GameObject prefab;

		public Damage damage;

		[MinMax(0, GameManager.ProjectileAgeLimit)]
		public FloatRange lifeSpan = new FloatRange(20, 30);

		public virtual float GetAcceleration(StatSheet statSheet) => 0;

		public virtual float GetVelocity(StatSheet statSheet) => 0;

		public virtual GameObject Spawn(StatSheet statSheet, Vector3 position, Vector3 heading)
		{
			var obj = ObjectPool.Spawn(prefab, new PositionSpawner(position));
			obj.transform.up = heading;

			if (obj.TryGetComponent<IProjectile>(out var projectile))
			{
				projectile.DeathTime = Time.time + lifeSpan.Random;
				projectile.Damage = damage;
			}

			return obj;
		}
	}
}
