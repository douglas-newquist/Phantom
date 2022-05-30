using UnityEngine;

namespace Game
{
	public class SwivelTurret : Turret
	{
		public GameObject head;

		[Range(0, GameManager.RotationSpeedLimit)]
		public float degreePerSec;

		public float MaxDeltaDegrees => degreePerSec * Time.deltaTime;

		protected float angle;

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
			head.transform.up = Math.RotateVector2(head.transform.up, deltaAngle * Mathf.Deg2Rad);

			return Vector3.SignedAngle(vector.normalized, head.transform.up, Vector3.forward);
		}

		public override void Reset()
		{
			Look(transform.up, Reference.Relative);
		}

		private void Update()
		{
			var mousePos = Input.mousePosition;
			var pos = Camera.main.ScreenToWorldPoint(mousePos);
			var angle = Look(pos, Reference.Absolute);
		}
	}
}
