using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public class ThrusterController : MonoBehaviour, IMover
	{
		public Rigidbody2D body;

		public Vector2 Velocity => body.velocity;

		public float Speed => Velocity.magnitude;

		private Thruster[] thrusters;

		private Vector2 force;

		[Range(0f, 1f)]
		public float brakeMaxVelocityToSetZero = 0.1f;

		[Header("Collision Avoidance")]
		public float lookAheadRange = 2;

		[Range(0f, 10f)]
		public float collisionAvoidanceStrength = 0.2f;

		[Range(0, 16)]
		public int collisionExtraRays = 4;

		[Range(0f, 90f)]
		public float collisionRayAngle = 45;

		public float CollisionRayAngle => collisionRayAngle;

		public float DeltaRayAngle => CollisionRayAngle / collisionExtraRays;

		public float RayDistance => Speed * lookAheadRange;

		public int TotalCollisionRays => 1 + collisionExtraRays * 2;

		public IEnumerable<Vector2> CollisionRayDirections
		{
			get
			{
				var mainRay = body.velocity.normalized;
				yield return mainRay;
				var deltaAngle = DeltaRayAngle * Mathf.Deg2Rad;

				for (int i = 0; i < collisionExtraRays; i++)
				{
					yield return Math.RotateVector2(mainRay, deltaAngle * i);
					yield return Math.RotateVector2(mainRay, -deltaAngle * i);
				}
			}
		}

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

		public RaycastHit2D CastCollisionRay(Vector2 direction)
		{
			return Physics2D.Raycast(transform.position, direction.normalized, RayDistance);
		}


		public Vector2 GetCollisionRayPush(RaycastHit2D hit, Vector2 direction)
		{
			if (hit.transform == null) return Vector2.zero;
			if (hit.fraction != 0)
				direction /= hit.fraction;
			return -direction * collisionAvoidanceStrength / TotalCollisionRays;
		}

		public Vector2 GetCollisionAvoidancePush()
		{
			Vector2 push = Vector2.zero;

			foreach (var ray in CollisionRayDirections)
			{
				var hit = CastCollisionRay(ray);
				push += GetCollisionRayPush(hit, ray);
				Debug.Log(GetCollisionRayPush(hit, ray));
			}

			return push;
		}

		public void Move(Vector2 vector, Reference mode)
		{
			vector = TranslateVector(vector, mode);
			vector += GetCollisionAvoidancePush();

			foreach (var thruster in thrusters)
				force += thruster.Thrust(vector, mode);
		}

		public void MoveTo(Vector2 position)
		{
			throw new System.NotImplementedException();
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

		private void OnDrawGizmos()
		{
			var _color = Gizmos.color;
			if (body == null) return;

			foreach (var direction in CollisionRayDirections)
			{
				var hit = CastCollisionRay(direction);
				var push = GetCollisionRayPush(hit, direction);
				if (hit.transform != null)
				{
					Gizmos.color = Color.red;
					Gizmos.DrawRay(transform.position, hit.point - (Vector2)transform.position);
					Gizmos.DrawRay(transform.position, push);
				}
				else
				{
					Gizmos.color = Color.white;
					Gizmos.DrawRay(transform.position, direction * RayDistance);
				}
			}

			Gizmos.color = _color;
		}
	}
}
