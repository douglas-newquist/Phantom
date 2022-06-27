using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class TurretGroup : IWeapon
	{
		public List<Turret> turrets = new List<Turret>();

		public int Count => turrets.Count;

		public IEnumerable<Projectile> Fire()
		{
			var projectiles = new List<Projectile>();

			foreach (var turret in turrets)
				projectiles.AddRange(turret.Fire());

			return projectiles;
		}

		/// <summary>
		/// Fires this group of turrets at the given vector
		/// </summary>
		/// <param name="vector">Vector to fire at</param>
		/// <param name="mode">What the vector is relative too</param>
		/// <returns>List of projectiles fired</returns>
		public IEnumerable<Projectile> Fire(Vector2 vector, Reference mode)
		{
			var projectiles = new List<Projectile>();

			foreach (var turret in turrets)
				projectiles.AddRange(turret.Fire(vector, mode));

			return projectiles;
		}

		public IEnumerable<Projectile> Fire(Rigidbody2D target)
		{
			var projectiles = new List<Projectile>();

			foreach (var turret in turrets)
				projectiles.AddRange(turret.Fire(target));

			return projectiles;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="vector">Vector to look at</param>
		/// <param name="mode">What the vector is relative too</param>
		public float Aim(Vector2 vector, Reference mode)
		{
			float delta = 0;

			foreach (var turret in turrets)
				delta += Mathf.Abs(turret.Aim(vector, mode));

			return delta / Count;
		}

		public float Aim(Rigidbody2D target)
		{
			float delta = 0;

			foreach (var turret in turrets)
				delta += Mathf.Abs(turret.Aim(target));

			return delta / Count;
		}

		public void Reset()
		{
			foreach (var turret in turrets)
				turret.Reset();
		}
	}
}
