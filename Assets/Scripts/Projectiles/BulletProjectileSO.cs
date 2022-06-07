using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Projectiles/Bullet")]
	public class BulletProjectileSO : ProjectileSO
	{
		[MinMax(0, GameManager.SpeedLimit)]
		public FloatRange velocity = new FloatRange(1, 1);

		public StatSO velocityStat;

		public override float GetVelocity(StatSheet statSheet)
		{
			var vel = velocity.Random;
			if (velocityStat != null)
				vel *= statSheet.GetValue(velocityStat);
			return vel;
		}
	}
}
