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

		public virtual void Reset() { }
	}
}
