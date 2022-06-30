using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class CollisionAvoidance
	{
		[SerializeField]
		private float lookAheadRange = 2;

		public float LookAheadRange
		{
			get => lookAheadRange;
			set => lookAheadRange = Mathf.Clamp(value, 0, float.MaxValue);
		}

		[Range(0f, 10f)]
		[SerializeField]
		private float strength = 1f;

		public float Strength
		{
			get => strength;
			set => strength = Mathf.Clamp(value, 0f, 10f);
		}

		[Range(0, 16)]
		[SerializeField]
		private int extraRays = 6;

		public int ExtraRays
		{
			get => extraRays;
			set => extraRays = Mathf.Clamp(value, 0, 16);
		}

		[Range(0f, 90f)]
		[SerializeField]
		private float totalRayAngle = 30;

		public float TotalRayAngle
		{
			get => totalRayAngle;
			set => totalRayAngle = Mathf.Clamp(value, 0f, 90f);
		}

		public float DeltaRayAngle => TotalRayAngle / ExtraRays;

		public int TotalCollisionRays => 1 + ExtraRays * 2;

		public float GetMaxRayDistance(Rigidbody2D body)
		{
			return body.velocity.magnitude * LookAheadRange;
		}

		/// <summary>
		/// Gets all the normalized ray directions for the given body
		/// </summary>
		private IEnumerable<Vector2> GetRayDirections(Rigidbody2D body)
		{
			var mainRay = body.velocity.normalized;
			yield return mainRay;
			var deltaAngle = DeltaRayAngle * Mathf.Deg2Rad;

			for (int i = 0; i < ExtraRays; i++)
			{
				yield return Math.RotateVector2(mainRay, deltaAngle * i);
				yield return Math.RotateVector2(mainRay, -deltaAngle * i);
			}
		}

		public RaycastHit2D CastRay(Rigidbody2D body, Vector2 direction, float maxDistance)
		{
			return Physics2D.Raycast(body.position, direction.normalized, maxDistance);
		}

		/// <summary>
		/// Casts the given ray from the given body
		/// </summary>
		/// <param name="body">Where to cast from</param>
		/// <param name="direction">Direction to cast in</param>
		private RaycastHit2D CastRay(Rigidbody2D body, Vector2 direction)
		{
			return Physics2D.Raycast(body.position, direction.normalized, GetMaxRayDistance(body));
		}

		/// <summary>
		/// Gets how much the given ray wants to push the body
		/// </summary>
		/// <param name="body">Body being used</param>
		/// <param name="hit">Where the ray hit</param>
		private Vector2 GetRayPush(Rigidbody2D body, RaycastHit2D hit)
		{
			if (hit.transform == null) return Vector2.zero;
			var direction = hit.normal * Strength;
			direction /= hit.fraction * TotalCollisionRays;
			return direction;
		}

		/// <summary>
		/// Calculates the total collision avoidance push
		/// </summary>
		public Vector2 GetCollisionPush(Rigidbody2D body)
		{
			Vector2 push = Vector2.zero;

			foreach (var ray in GetRayDirections(body))
			{
				var hit = CastRay(body, ray);
				push += GetRayPush(body, hit);
			}

			return push;
		}

		public void DrawGizmos(Rigidbody2D body)
		{
			var _color = Gizmos.color;
			if (body == null) return;

			foreach (var direction in GetRayDirections(body))
			{
				var hit = CastRay(body, direction);
				if (hit.transform != null)
				{
					Gizmos.color = Color.red;
					Gizmos.DrawRay(body.position, hit.point - (Vector2)body.position);
				}
				else
				{
					Gizmos.color = Color.white;
					Gizmos.DrawRay(body.position, direction * GetMaxRayDistance(body));
				}
			}

			Gizmos.color = _color;
		}
	}
}
