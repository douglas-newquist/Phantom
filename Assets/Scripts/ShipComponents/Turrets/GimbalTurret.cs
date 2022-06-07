using UnityEngine;

namespace Phantom
{
	public class GimbalTurret : SwivelTurret
	{
		[Range(0f, 180f)]
		public float degreeLimit = 25;

		public override float Look(Vector3 vector, Reference mode)
		{
			vector.z = head.transform.position.z;

			switch (mode)
			{
				case Reference.Absolute:
					vector -= head.transform.position;
					break;
			}

			var angle = Vector3.SignedAngle(vector.normalized, head.transform.up, Vector3.forward);
			var deltaAngle = -Mathf.Sign(angle) * Mathf.Min(MaxDeltaDegrees, Mathf.Abs(angle));
			var up = Math.RotateVector2(head.transform.up, deltaAngle * Mathf.Deg2Rad);

			angle = Vector3.SignedAngle(up, transform.up, Vector3.forward);

			if (Mathf.Abs(angle) > degreeLimit)
				up = Math.RotateVector2(transform.up, -Mathf.Sign(angle) * degreeLimit * Mathf.Deg2Rad);

			head.transform.up = up;

			return Vector3.Angle(vector.normalized, head.transform.up);
		}
	}
}
