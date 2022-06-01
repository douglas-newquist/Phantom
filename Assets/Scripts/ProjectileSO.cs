using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Projectile")]
	public class ProjectileSO : ScriptableObject
	{
		public GameObject prefab;

		public Damage damage;

		[Range(0, GameManager.SpeedLimit)]
		public float velocity = 1;

		public StatSO velocityStat;

		[Range(0, GameManager.SpeedLimit)]
		public float acceleration = 0;

		public StatSO accelerationStat;

		public virtual float GetVelocity(StatSheet statSheet)
		{
			var vel = velocity;
			if (velocityStat != null)
				vel *= statSheet.GetValue(velocityStat);
			return Mathf.Clamp(vel, 0, GameManager.SpeedLimit);
		}

		public virtual float GetAcceleration(StatSheet statSheet)
		{
			var accel = acceleration;
			if (accelerationStat != null)
				accel *= statSheet.GetValue(accelerationStat);
			return accel;
		}

		public virtual GameObject Spawn(StatSheet statSheet, Vector3 position, Vector3 heading)
		{
			var obj = Instantiate(prefab, position, Quaternion.identity);
			obj.transform.up = heading;
			var body = obj.GetComponent<Rigidbody2D>();
			body.velocity = heading * GetVelocity(statSheet);
			return obj;
		}
	}
}
