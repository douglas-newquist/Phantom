using UnityEngine;

namespace Game
{
	public class MissileProjectile : BulletProjectile
	{
		public MissileProjectileSO MissileStats => (MissileProjectileSO)ProjectileStats;

		protected virtual void FixedUpdate()
		{
			Vector3 velocity = body.velocity;
			velocity += transform.up * Time.fixedDeltaTime * ProjectileStats.GetAcceleration(statSheet);

			body.velocity = velocity;
		}
	}
}
