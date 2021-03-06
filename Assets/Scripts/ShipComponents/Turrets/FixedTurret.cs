using UnityEngine;

namespace Phantom
{
	public class FixedTurret : Turret
	{
		public override Vector3 Forward => transform.up;

		public override Vector3 Position => transform.position;

		public override float Aim(Vector2 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Absolute:
					vector -= (Vector2)Position;
					break;
			}

			return Vector3.Angle(Forward, vector);
		}
	}
}
