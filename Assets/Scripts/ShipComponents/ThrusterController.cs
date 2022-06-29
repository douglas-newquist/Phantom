using System.Collections.Generic;
using UnityEngine;
using Phantom.Pathfinding;

namespace Phantom
{
	public class ThrusterController : MonoBehaviour, IMover
	{
		public Rigidbody2D body;

		public Vector2 Velocity
		{
			get => body.velocity;
			set => body.velocity = value;
		}

		public float Speed => Velocity.magnitude;

		private Thruster[] thrusters;

		private Vector2 force;

		[Range(0f, 1f)]
		public float brakeMaxVelocityToSetZero = 0.1f;

		[Range(0, Level.TileSize)]
		public float moveToBrakeDistance = 2f;

		public CollisionAvoidance collisionAvoidance = new CollisionAvoidance();

		public VectorPIDController PID = new VectorPIDController(1, 0, 0);

		public Vector2 target;

		private void Start()
		{
			body = GetComponent<Rigidbody2D>();
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
			vector += collisionAvoidance.GetCollisionPush(body);

			foreach (var thruster in thrusters)
				force += thruster.Thrust(vector, mode);
		}

		public void MoveTo(Vector2 position)
		{
			var delta = position - (Vector2)transform.position;
			var direction = delta.normalized;
			var hit = collisionAvoidance.CastRay(body, direction, delta.magnitude);

			if (Vector2.Distance(position, target) > Level.TileSize)
				PID.Reset();

			target = position;

			if (delta.magnitude < moveToBrakeDistance)
			{
				Brake();
			}
			else if (hit.transform == null)
			{
				var error = target - (Vector2)transform.position;
				var move = PID.Correction(error, Time.fixedDeltaTime);
				Move(move, Reference.Absolute);
			}
			else
			{
				Debug.LogWarning("Pathfinder needed");
			}
		}

		/// <summary>
		/// Brings the ship's velocity to zero
		/// </summary>
		public void Brake()
		{
			if (Speed <= brakeMaxVelocityToSetZero)
			{
				Velocity = Vector2.zero;
				return;
			}

			var move = PID.Correction(-Velocity, Time.fixedDeltaTime);
			Move(move, Reference.Absolute);
		}

		private void FixedUpdate()
		{
			body.AddForce(force * Time.fixedDeltaTime, ForceMode2D.Impulse);
			force = Vector2.zero;
			if (body.velocity.magnitude > GameManager.SpeedLimit)
				body.velocity = body.velocity.normalized * GameManager.SpeedLimit;
		}

		private void OnDrawGizmos()
		{
			if (body != null)
				collisionAvoidance.DrawGizmos(body);

			Gizmos.DrawSphere(target, Level.TileSize / 4);
		}
	}
}
