using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public class ThrusterController : MonoBehaviour
	{
		public Rigidbody2D body => GetComponent<Rigidbody2D>();

		public Thruster[] thrusters => GetComponentsInChildren<Thruster>();

		private Vector2 force;

		public void Move(Vector3 vector, Reference mode)
		{
			if (vector.sqrMagnitude > 1)
				vector.Normalize();

			switch (mode)
			{
				case Reference.Relative:
					vector = transform.TransformDirection(vector);
					break;
			}

			foreach (var thruster in thrusters)
				force += thruster.Thrust(vector, mode);
		}

		/// <summary>
		/// Brings the ship's velocity to zero
		/// </summary>
		public void Stop()
		{
			Move(-body.velocity, Reference.Absolute);
		}

		private void FixedUpdate()
		{
			body.AddForce(force * Time.fixedDeltaTime, ForceMode2D.Impulse);
			force = Vector2.zero;
			if (body.velocity.magnitude > GameManager.SpeedLimit)
				body.velocity = body.velocity.normalized * GameManager.SpeedLimit;
		}
	}
}
