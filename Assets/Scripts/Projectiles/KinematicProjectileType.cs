using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Projectile + "Bullet")]
	public class KinematicProjectileType : ProjectileType
	{
		[MinMax(0, GameManager.ProjectileSpeedLimit)]
		public FloatRange velocity = new FloatRange(1, 1);

		public StatType velocityStat;

		[MinMax(0, GameManager.SpeedLimit)]
		public FloatRange acceleration = 0;

		public StatType accelerationStat;

		public override float GetAcceleration(StatSheet statSheet)
		{
			if (accelerationStat == null)
				return acceleration.Random;
			return acceleration.Random * statSheet.GetValue(accelerationStat);
		}

		public override float GetVelocity(StatSheet statSheet)
		{
			if (velocityStat == null)
				return velocity.Random;
			return velocity.Random * statSheet.GetValue(velocityStat);
		}

		public override GameObject Spawn(StatSheet statSheet, Vector3 position, Vector3 heading)
		{
			var obj = base.Spawn(statSheet, position, heading);

			if (obj.TryGetComponent<Rigidbody2D>(out var body))
				body.velocity = heading * GetVelocity(statSheet);

			if (obj.TryGetComponent<IProjectile>(out var projectile))
				projectile.Acceleration = GetAcceleration(statSheet);

			return obj;
		}
	}
}
