using UnityEngine;

namespace Game
{
	public class FixedTurret : Turret, ITurret
	{
		public override Vector3 Forward => transform.up;

		public override Vector3 Position => transform.position;

		public override float Look(Vector3 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Absolute:
					vector -= Position;
					break;
			}

			return Vector3.Angle(Forward, vector);
		}
	}
}
