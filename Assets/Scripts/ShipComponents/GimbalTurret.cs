using UnityEngine;

namespace Game
{
	public class GimbalTurret : Turret, ITurret
	{
		public GameObject head;

		public override float Look(Vector3 vector, Reference mode)
		{
			throw new System.NotImplementedException();

			return Vector3.Angle(transform.up, vector);
		}

		public override void Reset()
		{
			throw new System.NotImplementedException();
		}
	}
}
