using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Projectile + "Missile")]
	public class MissileProjectileSO : BulletProjectileSO
	{
		[MinMax(0, GameManager.SpeedLimit)]
		public FloatRange acceleration = 0;

		public StatType accelerationStat;

		public override float GetAcceleration(StatSheet statSheet)
		{
			var accel = acceleration.Random;
			if (accelerationStat != null)
				accel *= statSheet.GetValue(accelerationStat);
			return accel;
		}
	}
}
