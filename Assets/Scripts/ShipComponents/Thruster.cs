using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public class Thruster : ShipComponent
	{
		public StatSO thrustStat;

		public float force = 1;

		public Vector2 Thrust(Vector3 vector, Reference mode)
		{
			return vector * statSheet.GetValue(thrustStat) * force;
		}
	}
}
