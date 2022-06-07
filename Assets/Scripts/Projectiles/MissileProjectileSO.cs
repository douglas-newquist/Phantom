using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Projectiles/Missile")]
	public class MissileProjectileSO : BulletProjectileSO
	{
		[MinMax(0, GameManager.SpeedLimit)]
		public FloatRange acceleration = 0;

		public StatSO accelerationStat;

		public override float GetAcceleration(StatSheet statSheet)
		{
			var accel = acceleration.Random;
			if (accelerationStat != null)
				accel *= statSheet.GetValue(accelerationStat);
			return accel;
		}
	}
}
