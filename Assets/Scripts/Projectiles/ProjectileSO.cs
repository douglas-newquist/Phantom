using UnityEngine;

namespace Game
{
	public abstract class ProjectileSO : ScriptableObject
	{
		public GameObject prefab;

		public Damage damage;

		public float lifeSpan = 60;

		public virtual float GetVelocity(StatSheet statSheet) => 0;

		public virtual float GetAcceleration(StatSheet statSheet) => 0;

		public virtual Projectile Spawn(StatSheet statSheet, Vector3 position, Vector3 heading)
		{
			var obj = Instantiate(prefab, position, Quaternion.identity);
			var projectile = obj.GetComponent<Projectile>();
			projectile.damage = damage;
			projectile.statSheet = statSheet;
			projectile.ProjectileStats = this;
			obj.transform.up = heading;
			var body = obj.GetComponent<Rigidbody2D>();
			body.velocity = heading * GetVelocity(statSheet);
			return projectile;
		}
	}
}
