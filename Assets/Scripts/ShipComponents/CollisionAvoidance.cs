using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class CollisionAvoidance
	{
		public float lookAheadRange = 2;

		[Range(0f, 10f)]
		public float strength = 1f;

		[Range(0, 16)]
		public int extraRays = 6;

		[Range(0f, 90f)]
		public float totalRayAngle = 30;

		public float DeltaRayAngle => totalRayAngle / extraRays;

		public int TotalCollisionRays => 1 + extraRays * 2;

		public float GetMaxRayDistance(Rigidbody2D body)
		{
			return body.velocity.magnitude * lookAheadRange;
		}

		/// <summary>
		/// Gets all the normalized ray directions for the given body
		/// </summary>
		public IEnumerable<Vector2> GetRayDirections(Rigidbody2D body)
		{
			var mainRay = body.velocity.normalized;
			yield return mainRay;
			var deltaAngle = DeltaRayAngle * Mathf.Deg2Rad;

			for (int i = 0; i < extraRays; i++)
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
		public RaycastHit2D CastRay(Rigidbody2D body, Vector2 direction)
		{
			return Physics2D.Raycast(body.position, direction.normalized, GetMaxRayDistance(body));
		}

		/// <summary>
		/// Gets how much the given ray wants to push the body
		/// </summary>
		/// <param name="body">Body being used</param>
		/// <param name="hit">Where the ray hit</param>
		public Vector2 GetRayPush(Rigidbody2D body, RaycastHit2D hit)
		{
			if (hit.transform == null) return Vector2.zero;
			var direction = hit.normal * strength;
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
					Gizmos.color = new Color(1f / hit.fraction, 0, 0);
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
