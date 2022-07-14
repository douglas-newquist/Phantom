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

		/// <summary>
		/// Gets the extra cost of going through a->b->c
		/// </summary>
		/// <typeparam name="TMap">Map type the agent understands</typeparam>
		/// <param name="a">Cell 1</param>
		/// <param name="b">Cell 2</param>
		/// <param name="c">Cell 3</param>
		/// <returns></returns>
		float GetSubPathExtraCost(TMap map, TCell a, TCell b, TCell c);

		/// <summary>
		/// Called when the pathfinder finishes
		/// </summary>
		/// <param name="path">Resultant path object</param>
		void OnFinishedPathFinding(Path<TCell> path);

		/// <summary>
		/// Called when a better sub path is found
		/// </summary>
		/// <typeparam name="TMap">Map type the agent understands</typeparam>
		/// <param name="original">The original source cell</param>
		/// <param name="current">The new better source cell</param>
		/// <param name="end">Next cell in the path</param>
		void OnBetterPathFound(TMap map, TCell original, TCell current, TCell end);
	}

	/// <summary>
	/// Defines an agent that knows how to path through a type of map
	/// </summary>
	/// <typeparam name="TMap">Map type the agent understands</typeparam>
	public interface IPathAgent<TMap> : IPathAgent<TMap, Vector2Int> { }
}
