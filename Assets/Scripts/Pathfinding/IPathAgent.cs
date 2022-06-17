using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	/// <summary>
	/// Defines an agent that knows how to path through a type of map
	/// </summary>
	/// <typeparam name="TMap">Map type the agent understands</typeparam>
	/// <typeparam name="TCell">Coordinates for cells on the map</typeparam>
	public interface IPathAgent<TMap, TCell>
	{
		/// <summary>
		/// Gets all reachable neighbor cells from the current position
		/// </summary>
		/// <typeparam name="TMap">Map type the agent understands</typeparam>
		/// <param name="pos">Where the agent is</param>
		/// <returns></returns>
		IEnumerable<TCell> GetNeighbors(TMap map, TCell pos);

		/// <summary>
		/// Gets the cost of moving from start to end
		/// </summary>
		/// <typeparam name="TMap">Map type the agent understands</typeparam>
		/// <param name="start">Cell to start in</param>
		/// <param name="end">Desired cell, may not be a neighbor</param>
		/// <returns></returns>
		float GetPathCost(TMap map, TCell start, TCell end);
	}

	/// <summary>
	/// Defines an agent that knows how to path through a type of map
	/// </summary>
	/// <typeparam name="TMap">Map type the agent understands</typeparam>
	public interface IPathAgent<TMap> : IPathAgent<TMap, Vector2Int> { }
}
