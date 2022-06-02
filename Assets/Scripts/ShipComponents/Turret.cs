using UnityEngine;

namespace Game
{
	public abstract class Turret : ShipComponent, ITurret
	{
		[Header("Fire Setting")]
		[Range(0f, 180f)]
		public float angleTolerance = 15;

		[Range(1f, 60f)]
		public float fireRate = 1;

		public float FireDelay => 1f / fireRate;

		private float nextShot = 0;

		public abstract Vector3 Forward { get; }

		public abstract Vector3 Position { get; }

		public bool CanFire => Time.time >= nextShot;

		public Vector3 PredictImpactLocation(Rigidbody2D target, ProjectileSO projectile)
		{
			return Math.PredictImpact(Position,
					target.position,
					projectile.GetVelocity(statSheet),
					target.velocity,
					projectile.GetAcceleration(statSheet),
					Vector2.zero);
		}

		public virtual Projectile Fire(ProjectileSO projectile)
		{
			if (CanFire)
			{
				nextShot = Time.time + fireRate;
				return projectile.Spawn(statSheet, Position, Forward);
			}

			return null;
		}

		public virtual Projectile Fire(Vector3 vector, ProjectileSO projectile, Reference mode)
		{
			var angle = Look(vector, mode);

			if (CanFire && Mathf.Abs(angle) <= angleTolerance)
				return Fire(projectile);

			return null;
		}

		public virtual Projectile Fire(Rigidbody2D target, ProjectileSO projectile)
		{
			var location = PredictImpactLocation(target, projectile);
			return Fire(location, projectile, Reference.Absolute);
		}

		public abstract float Look(Vector3 vector, Reference mode);

		public virtual void Reset() { }
	}
}
