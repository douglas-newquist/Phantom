using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	public abstract class ProjectileSO : ScriptableObject
	{
		public GameObject prefab;

		public Damage damage;

		[MinMax(0, GameManager.ProjectileAgeLimit)]
		public FloatRange lifeSpan = new FloatRange(20, 30);

		public virtual float GetLifeSpan(StatSheet statSheet) => lifeSpan.Random;

		public virtual float GetVelocity(StatSheet statSheet) => 0;

		public virtual float GetAcceleration(StatSheet statSheet) => 0;

		public virtual Projectile Spawn(StatSheet statSheet, Vector3 position, Vector3 heading)
		{
			var obj = ObjectPool.Spawn(prefab, new PositionSpawner(position));
			var projectile = obj.GetComponent<Projectile>();
			projectile.damage = damage;
			projectile.statSheet = statSheet;
			projectile.ProjectileStats = this;
			projectile.DeathTime = Time.time + GetLifeSpan(statSheet);
			obj.transform.up = heading;
			var body = obj.GetComponent<Rigidbody2D>();
			body.velocity = heading * GetVelocity(statSheet);
			return projectile;
		}
	}
}
