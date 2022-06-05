using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(StatSheet))]
	public class Ship : Entity
	{
		public ThrusterController thrusters;

		public GyroController gyros;

		public TurretController turrets;
	}
}
