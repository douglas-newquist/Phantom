using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class TurretGroup
	{
		public ProjectileSO projectile;

		public List<Turret> turrets = new List<Turret>();

		/// <summary>
		/// Fires this group of turrets at the given vector
		/// </summary>
		/// <param name="vector">Vector to fire at</param>
		/// <param name="mode">What the vector is relative too</param>
		/// <returns>List of projectiles fired</returns>
		public List<Projectile> Fire(Vector3 vector, Reference mode)
		{
			var projectiles = new List<Projectile>();

			foreach (var turret in turrets)
			{
				var p = turret.Fire(vector, projectile, mode);
				if (p != null)
					projectiles.Add(p);
			}

			return projectiles;
		}

		public List<Projectile> Fire(Rigidbody2D target)
		{
			var projectiles = new List<Projectile>();

			foreach (var turret in turrets)
			{
				var p = turret.Fire(target, projectile);
				if (p != null)
					projectiles.Add(p);
			}

			return projectiles;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="vector">Vector to look at</param>
		/// <param name="mode">What the vector is relative too</param>
		public void Look(Vector3 vector, Reference mode)
		{
			foreach (var turret in turrets)
				turret.Look(vector, mode);
		}

		public Vector3 PredictImpactLocation(Rigidbody2D target, Vector3 acceleration)
		{
			return target.position;
		}

		public void Reset()
		{
			foreach (var turret in turrets)
				turret.Reset();
		}
	}
}
