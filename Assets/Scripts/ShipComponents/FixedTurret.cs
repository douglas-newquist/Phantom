using UnityEngine;

namespace Game
{
	public class FixedTurret : Turret, ITurret
	{
		public override float Look(Vector3 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Absolute:
					vector -= transform.position;
					break;
			}

			return Vector3.Angle(transform.up, vector);
		}
	}
}
