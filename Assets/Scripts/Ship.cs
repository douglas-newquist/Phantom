using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(StatSheet))]
	public class Ship : Entity
	{
		public ThrusterController thrusters;

		public GyroController gyros;

		public TurretController turrets;

		private void Update()
		{
			var x = Input.GetAxis("Horizontal");
			var y = Input.GetAxis("Vertical");
			if (Input.GetKey(KeyCode.X))
				thrusters.Stop();
			else
				thrusters.Move(new Vector3(x, y, 0), Reference.Relative);
		}
	}
}
