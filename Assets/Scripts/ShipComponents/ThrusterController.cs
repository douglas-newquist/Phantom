using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	public class ThrusterController : MonoBehaviour, IMover
	{
		public enum Goal
		{
			Direction,
			Position,
			Brake
		}

		private Goal goal;

		public Rigidbody2D body;

		private StatSheet statSheet;

		[SerializeField]
		private StatType massStat, thrustStat;

		public StatType MassStat => massStat;

		public StatType ThrustStat => thrustStat;

		public float Thrust => statSheet.GetValue(thrustStat);

		public float Mass => statSheet.GetValue(MassStat);

		public float Acceleration => Thrust / Mass;

		public Vector2 Velocity
		{
			get => body.velocity;
			set => body.velocity = value;
		}

		public float Speed => Velocity.magnitude;

		[Range(0f, 1f)]
		public float brakeMaxVelocityToSetZero = 0.1f;

		[Range(0, Level.TileSize)]
		public float moveToBrakeDistance = 2f;

		public CollisionAvoidance collisionAvoidance = new CollisionAvoidance();

		public VectorPIDController PID = new VectorPIDController(1, 0, 0);

		public VectorPIDController BrakePID = new VectorPIDController(0.75f, 0, 0);

		public VertexPathSeeker pathSeeker = new VertexPathSeeker();

		private Vector2 Target { get; set; }

		private void Start()
		{
			statSheet = GetComponent<StatSheet>();
			body = GetComponent<Rigidbody2D>();
		}

		/// <summary>
		/// Converts the given thrust vector to the corresponding frame of reference
		/// </summary>
		/// <param name="vector">Vector to translate</param>
		/// <param name="mode">Frame of reference</param>
		public Vector2 TranslateVector(Vector2 vector, Reference mode)
		{
			switch (mode)
			{
				case Reference.Relative:
					return transform.TransformDirection(vector);

				default:
					return vector;
			}
		}

		public void MoveRelative(Vector2 vector, Reference mode)
		{
			goal = Goal.Direction;

			if (vector.magnitude > 1)
				vector.Normalize();

			Target = TranslateVector(vector, mode);
		}

		public float MoveTo(Vector2 position)
		{
			if (goal != Goal.Position)
				PID.Reset();

			goal = Goal.Position;
			Target = position;

			return Vector2.Distance(transform.position, position);
		}

		/// <summary>
		/// Brings the ship's velocity to zero
		/// </summary>
		public void Brake()
		{
			if (goal != Goal.Brake)
				BrakePID.Reset();

			goal = Goal.Brake;
		}

		private void FixedUpdate()
		{
			Vector2 direction = Vector2.zero;

			switch (goal)
			{
				case Goal.Direction:
					direction = Target;
					break;

				case Goal.Position:
					direction = pathSeeker.GetWaypoint(transform.position, Target);
					if (Vector2.Distance(transform.position, direction) < moveToBrakeDistance
						&& Speed < brakeMaxVelocityToSetZero)
					{
						Velocity = Vector2.zero;
						return;
					}
					direction = PID.Correction(transform.position, direction, Time.fixedDeltaTime);
					break;

				case Goal.Brake:
					if (Speed < brakeMaxVelocityToSetZero)
					{
						Velocity = Vector2.zero;
						return;
					}

					direction = BrakePID.Correction(Velocity, Vector2.zero, Time.fixedDeltaTime);
					break;
			}

			direction += collisionAvoidance.GetCollisionPush(body);

			if (direction.magnitude == 0)
				return;

			if (direction.magnitude > 1)
				direction.Normalize();

			Velocity += direction * Acceleration * Time.fixedDeltaTime;

			if (Velocity.magnitude > GameManager.SpeedLimit)
				Velocity = Velocity.normalized * GameManager.SpeedLimit;
		}

		private void OnDrawGizmosSelected()
		{
			if (body != null)
				collisionAvoidance.DrawGizmos(body);

			pathSeeker.DrawGizmos();

			Gizmos.DrawSphere(Target, Level.TileSize / 4);
		}
	}
}
