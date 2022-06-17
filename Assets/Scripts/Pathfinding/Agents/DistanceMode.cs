namespace Phantom.Pathfinding
{
	public enum DistanceMode
	{
		/// <summary>
		/// dx + dy
		/// </summary>
		Manhattan,

		/// <summary>
		/// (dx)^2 + (dy)^2
		/// </summary>
		Square,

		/// <summary>
		/// Sqrt(dx^2 + dy^2)
		/// </summary>
		SquareRoot
	}
}
