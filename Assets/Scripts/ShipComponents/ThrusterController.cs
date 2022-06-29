using System.Collections.Generic;
using UnityEngine;
using Phantom.Pathfinding;

namespace Phantom
{
	public class ThrusterController : MonoBehaviour, IMover
	{
		public enum Goal
		{
			Direction,
			Position,
			Brake,
			PathFinding
		}

		private Goal goal;

		public Rigidbody2D body;

		public Vector2 Velocity
		{
			get => body.velocity;
			set => body.velocity = value;
		}

		public float Speed => Velocity.magnitude;

		private Thruster[] thrusters;

		[Range(0f, 1f)]
		public float brakeMaxVelocityToSetZero = 0.1f;

		[Range(0, Level.TileSize)]
		public float moveToBrakeDistance = 2f;

		public CollisionAvoidance collisionAvoidance = new CollisionAvoidance();

		public VectorPIDController PID = new VectorPIDController(1, 0, 0);

		public VertexPathAgent pathAgent;

		public Path<Vector2Int> path;

		[Range(0f, Level.TileSize)]
		public float pathFollowTolerance = Level.TileSize / 2;

		public Vector2 Target { get; private set; }

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

		public void MoveRelative(Vector2 vector, Reference mode)
		{
			goal = Goal.Direction;

			if (vector.magnitude > 1)
				vector.Normalize();

			if (mode == Reference.Relative)
				Target = transform.TransformDirection(vector);
			else
				Target = vector;
		}

		public float MoveTo(Vector2 position)
		{
			if (goal != Goal.Position && goal != Goal.PathFinding)
				PID.Reset();

			var delta = position - (Vector2)transform.position;
			var direction = delta.normalized;
			var hit = collisionAvoidance.CastRay(body, direction, delta.magnitude);

			if (hit.transform == null)
			{
				goal = Goal.Position;
				Target = position;
			}
			else
			{
				if (Target != position)
				{
					Target = position;
					goal = Goal.PathFinding;
					var start = (Vector2Int)GameManager.CurrentLevel.GetClosestVertex(transform.position);
					var end = (Vector2Int)GameManager.CurrentLevel.GetClosestVertex(position);
					path = pathAgent.FindPath(GameManager.CurrentLevel.Vertices, start, end);
				}
			}

			return Vector2.Distance(transform.position, position);
		}

		/// <summary>
		/// Brings the ship's velocity to zero
		/// </summary>
		public void Brake()
		{
			if (goal != Goal.Brake)
				PID.Reset();

			goal = Goal.Brake;
		}

		private void FixedUpdate()
		{
			Vector2 force = Vector2.zero;
			Vector2 direction = Vector2.zero;
			Vector2 target = Target;

			if (goal == Goal.PathFinding)
			{
				if (path.TryGetWaypoint(out var waypoint))
				{
					target = GameManager.CurrentLevel.GridToWorldPoint((Vector3Int)waypoint);
					if (Vector2.Distance(target, transform.position) < pathFollowTolerance)
						path.NextWaypoint();
				}

				if (path.TryGetWaypoint(out waypoint))
				{
					target = GameManager.CurrentLevel.GridToWorldPoint((Vector3Int)waypoint);
				}
				else
					target = transform.position;
			}

			switch (goal)
			{
				case Goal.Direction:
					direction = target;
					break;

				case Goal.PathFinding:
				case Goal.Position:
					if (Vector2.Distance(transform.position, target) < moveToBrakeDistance
						&& Speed < brakeMaxVelocityToSetZero)
					{
						Velocity = Vector2.zero;
						return;
					}
					direction = PID.Correction(transform.position, target, Time.fixedDeltaTime);
					break;

				case Goal.Brake:
					if (Speed < brakeMaxVelocityToSetZero)
					{
						Velocity = Vector2.zero;
						return;
					}

					direction = PID.Correction(Velocity, Vector2.zero, Time.fixedDeltaTime);
					break;
			}

			direction += collisionAvoidance.GetCollisionPush(body);

			if (direction.magnitude == 0)
				return;

			if (direction.magnitude > 1)
				direction.Normalize();

			foreach (var thruster in thrusters)
				force += thruster.Thrust(direction, Reference.Absolute);

			body.AddForce(force * Time.fixedDeltaTime, ForceMode2D.Impulse);

			if (body.velocity.magnitude > GameManager.SpeedLimit)
				body.velocity = body.velocity.normalized * GameManager.SpeedLimit;
		}

		private void OnDrawGizmos()
		{
			if (body != null)
				collisionAvoidance.DrawGizmos(body);

			if (path.Finished)
			{
				var start = GameManager.CurrentLevel.GridToWorldPoint(GameManager.CurrentLevel.GetClosestVertex(transform.position));
				Gizmos.DrawWireSphere(start, Level.TileSize / 4);
				var lastCell = transform.position;
				foreach (var cell in path)
				{
					var pos = GameManager.CurrentLevel.GridToWorldPoint((Vector3Int)cell);
					Gizmos.DrawWireSphere(pos, pathFollowTolerance);
					Gizmos.DrawLine(lastCell, pos);
					lastCell = pos;
				}
			}

			Gizmos.DrawSphere(Target, Level.TileSize / 4);
		}
	}
}
