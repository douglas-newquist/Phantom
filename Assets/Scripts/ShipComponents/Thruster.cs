using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	public class Thruster : ShipComponent
	{
		public StatType thrustStat;

		public float force = 1;

		public Vector2 GetMaximumThrust(Vector3 vector)
		{
			return vector * StatSheet.GetValue(thrustStat) * force;
		}

		public Vector2 Thrust(Vector3 vector, Reference mode)
		{
			return vector * StatSheet.GetValue(thrustStat) * force;
		}
	}
}
