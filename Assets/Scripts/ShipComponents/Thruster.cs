using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IThruster
	{

	}
	public class Thruster : ShipComponent, IThruster
	{
		public StatSO thrustStat;

		public float force = 1;

		public Vector2 Thrust(Vector3 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Relative:
					vector = transform.TransformDirection(vector);
					break;
			}

			return vector * statSheet.GetValue(thrustStat) * force;
		}
	}
}
