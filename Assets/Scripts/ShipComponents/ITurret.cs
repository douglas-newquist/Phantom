using UnityEngine;

namespace Game
{
	public interface ITurret
	{
		float ProjectileVelocity { get; }

		Vector3 PredictTargetPosition(Rigidbody2D body);

		float Look(Vector3 vector, Reference mode);

		bool Fire(Vector3 vector, Reference mode);

		/// <summary>
		/// Returns the turret to its default state
		/// </summary>
		void Reset();
	}
}
