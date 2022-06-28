using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Defines an object that can look at a point or direction
	/// </summary>
	public interface ILooker
	{
		Transform transform { get; }

		void Look(Vector2 vector2, Reference mode);
	}
}
