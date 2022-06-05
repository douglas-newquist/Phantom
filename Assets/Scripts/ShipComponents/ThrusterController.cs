using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class ThrusterController : MonoBehaviour
	{
		public Rigidbody2D body;

		public List<Thruster> thrusters = new List<Thruster>();

		private Vector2 force;

		public void Move(Vector3 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Relative:
					if (vector.sqrMagnitude > 1)
						vector.Normalize();
					foreach (var thruster in thrusters)
						force += thruster.Thrust(vector, mode);
					break;
			}
		}

		/// <summary>
		/// Brings the ship's velocity to zero
		/// </summary>
		public void Stop()
		{
			Move(-body.velocity, Reference.Relative);
		}

		private void FixedUpdate()
		{
			body.AddRelativeForce(force * Time.fixedDeltaTime, ForceMode2D.Impulse);
			force = Vector2.zero;
			if (body.velocity.magnitude > GameManager.SpeedLimit)
				body.velocity = body.velocity.normalized * GameManager.SpeedLimit;
		}
	}
}
