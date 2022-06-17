using UnityEngine;

namespace Phantom.Pathfinding
{
	public enum DiagonalMode
	{
		Disallow,
		Allow,

		[Tooltip("Allows diagonals if both orthogonal directions can be done")]
		AllowIfBothOrthogonal
	}
}
