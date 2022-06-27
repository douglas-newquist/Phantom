using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Defines an object that aim towards a point or target
	/// </summary>
	public interface IAim
	{
		/// <summary>
		///	Aims at the given point or direction
		/// </summary>
		/// <param name="vector">Vector to look at</param>
		/// <param name="mode">What the vector is relative too</param>
		/// <returns></returns>
		float Aim(Vector2 vector, Reference mode);

		/// <summary>
		/// Aims at the given rigid body
		/// </summary>
		/// <param name="target">Target to aim at</param>
		/// <returns></returns>
		float Aim(Rigidbody2D target);
	}
}
