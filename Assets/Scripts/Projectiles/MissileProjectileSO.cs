using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Projectiles/Missile")]
	public class MissileProjectileSO : BulletProjectileSO
	{
		[Range(0, GameManager.SpeedLimit)]
		public float acceleration = 0;

		public StatSO accelerationStat;

		public override float GetAcceleration(StatSheet statSheet)
		{
			var accel = acceleration;
			if (accelerationStat != null)
				accel *= statSheet.GetValue(accelerationStat);
			return accel;
		}
	}
}
