using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Projectile + "Bullet")]
	public class BulletProjectileSO : ProjectileSO
	{
		[MinMax(0, GameManager.SpeedLimit)]
		public FloatRange velocity = new FloatRange(1, 1);

		public StatType velocityStat;

		public override float GetVelocity(StatSheet statSheet)
		{
			var vel = velocity.Random;
			if (velocityStat != null)
				vel *= statSheet.GetValue(velocityStat);
			return vel;
		}
	}
}
