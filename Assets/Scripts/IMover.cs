using UnityEngine;

namespace Phantom
{
	public interface IMover
	{
		Transform transform { get; }

		/// <summary>
		/// Moves this object relative to a frame of reference
		/// </summary>
		/// <param name="vector">Reference vector to move</param>
		/// <param name="mode">Frame of reference to move in</param>
		void MoveRelative(Vector2 vector, Reference mode);

		/// <summary>
		/// Moves to the given position
		/// </summary>
		/// <param name="position"></param>
		void MoveTo(Vector2 position);

		void Brake();
	}
}
