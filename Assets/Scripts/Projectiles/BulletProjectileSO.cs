using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Projectiles/Bullet")]
	public class BulletProjectileSO : ProjectileSO
	{
		[Range(0, GameManager.SpeedLimit)]
		public float velocity = 1;

		public StatSO velocityStat;

		public override float GetVelocity(StatSheet statSheet)
		{
			var vel = velocity;
			if (velocityStat != null)
				vel *= statSheet.GetValue(velocityStat);
			return vel;
		}
	}
}
