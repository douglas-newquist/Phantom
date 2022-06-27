using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	public class SwivelTurret : Turret
	{
		[Header("Turret Head")]
		public GameObject head;

		[Range(0, GameManager.RotationSpeedLimit)]
		public float degreePerSec;

		public float DegreesPerSec => Mathf.Clamp(degreePerSec * StatSheet.GetValue(trackSpeed), 0, GameManager.RotationSpeedLimit);

		public float MaxDeltaDegrees => DegreesPerSec * Time.deltaTime;

		public override Vector3 Forward => head.transform.up;

		public override Vector3 Position => head.transform.position;

		public StatType trackSpeed;

		public override float Aim(Vector2 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Absolute:
					vector -= (Vector2)head.transform.position;
					break;
			}

			var angle = Vector3.SignedAngle(vector.normalized, head.transform.up, Vector3.forward);
			var deltaAngle = -Mathf.Sign(angle) * Mathf.Min(MaxDeltaDegrees, Mathf.Abs(angle));
			head.transform.up = Math.RotateVector2(head.transform.up, deltaAngle * Mathf.Deg2Rad);

			return Vector3.SignedAngle(vector.normalized, head.transform.up, Vector3.forward);
		}

		public override void Reset()
		{
			Aim(transform.up, Reference.Relative);
		}
	}
}
