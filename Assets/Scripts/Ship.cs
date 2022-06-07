using UnityEngine;

namespace Phantom
{
	[RequireComponent(typeof(StatSheet))]
	public class Ship : Entity
	{
		public ThrusterController thrusters;

		public GyroController gyros;

		public TurretController turrets;

		public Rigidbody2D target;

		public StatSO massStat;

		private void Update()
		{
			GetComponent<Rigidbody2D>().mass = Stats.GetValue(massStat);
			var x = Input.GetAxis("Horizontal");
			var y = Input.GetAxis("Vertical");
			var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouse.z = 0;

			if (Input.GetKey(KeyCode.X))
				thrusters.Stop();
			else
				thrusters.Move(new Vector3(x, y, 0), Reference.Relative);

			gyros.Look(mouse, Reference.Absolute);

			if (target != null)
				turrets.Aim(target, Input.GetMouseButton(0));
			else
				turrets.Aim(mouse, Reference.Absolute, Input.GetMouseButton(0));
		}
	}
}
