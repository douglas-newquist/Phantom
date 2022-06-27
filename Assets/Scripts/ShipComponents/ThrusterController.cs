using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public class ThrusterController : MonoBehaviour, IMover
	{
		public Rigidbody2D body => GetComponent<Rigidbody2D>();

		private Thruster[] thrusters;

		private Vector2 force;

		[Range(0f, 1f)]
		public float brakeMaxVelocityToSetZero = 0.1f;

		private void Start()
		{
			thrusters = GetComponentsInChildren<Thruster>();
		}

		/// <summary>
		/// Converts the given thrust vector to the corresponding frame of reference
		/// </summary>
		/// <param name="vector">Vector to translate</param>
		/// <param name="mode">Frame of reference</param>
		public Vector2 TranslateVector(Vector2 vector, Reference mode)
		{
			if (vector.sqrMagnitude > 1)
				vector.Normalize();

			switch (mode)
			{
				case Reference.Relative:
					return transform.TransformDirection(vector);

				default:
					return vector;
			}
		}

		public Vector2 GetMaximumThrust(Vector2 vector, Reference mode)
		{
			vector = TranslateVector(vector, mode);
			var max = Vector2.zero;

			foreach (var thruster in thrusters)
				max += thruster.GetMaximumThrust(vector);

			return max;
		}

		public void Move(Vector2 vector, Reference mode)
		{
			vector = TranslateVector(vector, mode);

			foreach (var thruster in thrusters)
				force += thruster.Thrust(vector, mode);
		}

		/// <summary>
		/// Brings the ship's velocity to zero
		/// </summary>
		public void Brake()
		{
			if (body.velocity.magnitude < brakeMaxVelocityToSetZero)
			{
				body.velocity = Vector2.zero;
				force = Vector2.zero;
				return;
			}

			var maxThrust = GetMaximumThrust(-body.velocity, Reference.Absolute);
			var maxDeltaV = maxThrust * Time.fixedDeltaTime / body.mass;

			if (body.velocity.magnitude > maxDeltaV.magnitude)
				Move(-body.velocity, Reference.Absolute);
			else
				Move(maxDeltaV, Reference.Absolute);
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
