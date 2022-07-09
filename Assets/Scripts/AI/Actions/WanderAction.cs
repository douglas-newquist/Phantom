using System.Collections;
using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Wanders around a specific point on the map
	/// </summary>
	public class WanderAction : Action, IAction
	{
		IMover mover;
		ILooker looker;
		Rigidbody2D body;

		private Vector2 origin, target;

		/// <summary>
		/// The point to wander around
		/// </summary>
		public Vector2 Origin
		{
			get => CenterOnSelf ? transform.position : origin;
			set
			{
				CenterOnSelf = false;
				origin = value;
			}
		}

		[SerializeField]
		[MinMax(0f, Level.TotalSizeLimit)]
		private FloatRange wanderDistance;

		/// <summary>
		/// How far to wander from the origin
		/// </summary>
		public FloatRange WanderDistance
		{
			get => wanderDistance;
			set => wanderDistance = value;
		}

		[SerializeField]
		[MinMax(0f, 5f * 60f)]
		private FloatRange idleTime = new FloatRange(0, 15);

		/// <summary>
		/// Time to wait at the target location
		/// </summary>
		public FloatRange IdleTime
		{
			get => idleTime;
			set => idleTime = value;
		}

		[SerializeField]
		private bool centerOnSelf = false;

		public bool CenterOnSelf
		{
			get => centerOnSelf;
			set => centerOnSelf = value;
		}

		private void OnSpawn()
		{
			origin = transform.position;
			mover = GetComponent<IMover>();
			looker = GetComponent<ILooker>();
			body = GetComponent<Rigidbody2D>();
		}

		protected override bool PreAction()
		{
			Vector2 dir = Random.insideUnitCircle.normalized;
			float dist = WanderDistance.Random;
			Vector2 point = Origin + dir * dist;
			Vector2 delta = point - (Vector2)transform.position;

			var hit = Physics2D.Raycast(transform.position, delta.normalized, delta.magnitude);
			if (hit.transform == null)
			{
				target = point;
			}
			else
				return false;

			return true;
		}

		private bool ReachedGoal()
		{
			looker.Look(target - (Vector2)transform.position, Reference.Absolute);
			return Vector2.Distance(transform.position, target) < Level.TileSize;
		}

		protected override IEnumerator DoAction()
		{
			mover.MoveTo(target);
			yield return new WaitUntil(ReachedGoal);
			yield return new WaitForSeconds(IdleTime.Random);
			IsRunning = false;
		}

		private void OnDrawGizmosSelected()
		{
			if (!IsRunning) return;

			var _color = Gizmos.color;
			Gizmos.color = Color.green;

			Gizmos.DrawWireSphere(Origin, WanderDistance.Max);
			Gizmos.DrawWireSphere(Origin, WanderDistance.Min);

			Gizmos.color = _color;
		}
	}
}
