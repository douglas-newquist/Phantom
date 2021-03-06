namespace Phantom.Pathfinding
{
	public enum PathStatus
	{
		/// <summary>
		/// Pathfinder is still searching for a path
		/// </summary>
		Searching,

		/// <summary>
		/// Pathfinder has finished and a path was found
		/// </summary>
		Found,

		/// <summary>
		/// Pathfinder has finished and no path is possible
		/// </summary>
		NoPathPossible,

		/// <summary>
		/// Pathfinder timed out
		/// </summary>
		TimedOut
	}
}
