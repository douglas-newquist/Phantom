using Phantom.StatSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Phantom
{
	public abstract class Turret : ShipComponent, IWeapon
	{
		[Header("Fire Setting")]
		public ProjectileSO projectile;

		[Range(0f, 180f)]
		public float angleTolerance = 15;

		[Range(1f, 60f)]
		public float fireRate = 1;

		public StatType fireRateStat;

		[Range(0f, 90f)]
		public float fireSpread = 5;

		public StatType fireSpreadStat;

		public float FireDelay => 1f / fireRate;

		private float nextShot = 0;

		public abstract Vector3 Forward { get; }

		public abstract Vector3 Position { get; }

		public bool CanFire => Time.time >= nextShot;

		public UnityEvent<ProjectileFiredEvent> OnProjectileFired;

		public Vector3 PredictImpactLocation(Rigidbody2D target)
		{
			return Math.PredictImpact(Position,
					target.position,
					projectile.GetVelocity(StatSheet),
					target.velocity,
					projectile.GetAcceleration(StatSheet),
					Vector2.zero);
		}

		public virtual IEnumerable<Projectile> Fire()
		{
			if (CanFire)
			{
				nextShot = Time.time + FireDelay;
				var p = projectile.Spawn(StatSheet, Position, Forward);
				OnProjectileFired.Invoke(new ProjectileFiredEvent(this, p));
				yield return p;
			}
		}

		public virtual IEnumerable<Projectile> Fire(Vector2 vector, Reference mode)
		{
			var angle = Aim(vector, mode);

			if (CanFire && Mathf.Abs(angle) <= angleTolerance)
				return Fire();

			return null;
		}

		public virtual IEnumerable<Projectile> Fire(Rigidbody2D target)
		{
			var location = PredictImpactLocation(target);
			return Fire(location, Reference.Absolute);
		}

		public abstract float Aim(Vector2 vector, Reference mode);

		public float Aim(Rigidbody2D target)
		{
			return Aim(target.transform.position, Reference.Absolute);
		}

		public virtual void Reset() { }
	}
}
