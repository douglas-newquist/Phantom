using System.Collections;
using UnityEngine;

namespace Phantom
{
	public class WanderAction : Action, IAction
	{
		IMover mover;
		ILooker looker;
		Rigidbody2D body;

		private Vector2 origin, target;

		[SerializeField]
		[MinMax(0f, Level.TotalSizeLimit)]
		private FloatRange wanderDistance;

		private void OnSpawn()
		{
			origin = transform.position;
			mover = GetComponent<IMover>();
			looker = GetComponent<ILooker>();
			body = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			if (!IsRunning)
				Perform();
		}

		protected override bool PreAction()
		{
			Vector2 dir = Random.insideUnitCircle.normalized;
			float dist = wanderDistance.Random;
			Vector2 point = origin + dir * dist;
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
			looker.Look(body.velocity.normalized, Reference.Absolute);
			return Vector2.Distance(transform.position, target) < Level.TileSize;
		}

		protected override IEnumerator DoAction()
		{
			mover.MoveTo(target);
			yield return new WaitUntil(ReachedGoal);
			IsRunning = false;
		}

		private void OnDrawGizmosSelected()
		{
			if (!IsRunning) return;

			var _color = Gizmos.color;
			Gizmos.color = Color.green;

			Gizmos.DrawWireSphere(origin, wanderDistance.Max);
			Gizmos.DrawWireSphere(-origin, wanderDistance.Min);

			Gizmos.color = _color;
		}
	}
}
