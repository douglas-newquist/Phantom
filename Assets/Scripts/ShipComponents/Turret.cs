using UnityEngine;

namespace Game
{
	public abstract class Turret : ShipComponent, ITurret
	{
		[Range(0f, 180f)]
		public float angleTolerance = 15;

		public ProjectileSO projectile;

		[Range(1f, 60f)]
		public float fireRate = 1;

		public float FireDelay => 1f / fireRate;

		private float nextShot = 0;

		public abstract Vector3 Forward { get; }

		public abstract Vector3 Position { get; }

		private Rigidbody2D lastTarget;
		private Vector2 lastTargetPos;

		public virtual Vector3 PredictTargetPosition(Rigidbody2D body)
		{
			if (lastTarget == null || lastTarget != body)
			{
				lastTarget = body;
				lastTargetPos = body.position;
			}

			var prediction = Math.PredictImpact(transform.position,
									  body.position,
									  projectile.GetVelocity(statSheet),
									  body.velocity,
									  projectile.GetAcceleration(statSheet),
									  body.position - lastTargetPos);

			lastTargetPos = body.position;
			return prediction;
		}

		public virtual Projectile Fire(Vector3 vector, ProjectileSO projectile, Reference mode)
		{
			var angle = Look(vector, mode);

			if (Time.time >= nextShot && Mathf.Abs(angle) <= angleTolerance)
			{
				nextShot = Time.time + fireRate;
				return projectile.Spawn(statSheet, Position, Forward);
			}

			return null;
		}

		public abstract float Look(Vector3 vector, Reference mode);

		/// <summary>
		/// Fires this turret at a moving target
		/// </summary>
		/// <param name="turret"></param>
		/// <param name="body">What moving target to fire at</param>
		/// <returns>True if the turret actually fired a projectile</returns>
		public virtual bool FireAt(Rigidbody2D body, ProjectileSO projectile)
		{
			return Fire(PredictTargetPosition(body), projectile, Reference.Absolute);
		}

		/// <summary>
		/// Points this turret at a moving target
		/// </summary>
		/// <param name="body">Target to track</param>
		/// <returns>The angle difference between this turret and the target</returns>
		public float LookAt(Rigidbody2D body)
		{
			return Look(PredictTargetPosition(body), Reference.Absolute);
		}

		public virtual void Reset() { }
	}
}
