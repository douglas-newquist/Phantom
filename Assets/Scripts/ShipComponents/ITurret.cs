using UnityEngine;

namespace Game
{
	public interface ITurret
	{
		float Look(Vector3 vector, Reference mode);

		Projectile Fire(Vector3 vector, ProjectileSO projectile, Reference mode);

		/// <summary>
		/// Returns the turret to its default state
		/// </summary>
		void Reset();
	}
}
