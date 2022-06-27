using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Defines an object that aim towards a point or target
	/// </summary>
	public interface IAim
	{
		void Aim(Vector2 vector, Reference mode);

		void Aim(Rigidbody2D target);
	}
}
