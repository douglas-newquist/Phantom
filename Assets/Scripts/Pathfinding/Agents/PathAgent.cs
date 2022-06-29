using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	public abstract class PathAgent<TMap, TCell> : ScriptableObject, IPathAgent<TMap, TCell>
	{
		public Pathfinder pathfinder;

		/// <summary>
		/// Gets the cost of moving through a specific cell
		/// </summary>
		/// <param name="map">Map being pathed through</param>
		/// <param name="pos">Cell be pathed over</param>
		public abstract float PathThroughCost(TMap map, TCell pos);

		/// <summary>
		/// Checks if the given cell can be pathed over
		/// </summary>
		/// <param name="map">Map being pathed through</param>
		/// <param name="pos">Cell be pathed over</param>
		public virtual bool CanPathThrough(TMap map, TCell pos)
		{
			return PathThroughCost(map, pos) >= 0;
		}

		public abstract IEnumerable<TCell> GetNeighbors(TMap map, TCell pos);

		public abstract float GetPathCost(TMap map, TCell start, TCell end);

		public Path<TCell> FindPath(TMap map, TCell start, TCell end)
		{
			if (pathfinder == null)
				throw new System.NullReferenceException("Pathfinder not assigned in PathAgent");

			return pathfinder.FindPath(this, map, start, end);
		}

		public Path<TCell> FindPathAsync(TMap map, TCell start, TCell end)
		{
			if (pathfinder == null)
				throw new System.NullReferenceException("Pathfinder not assigned in PathAgent");

			return pathfinder.FindPathAsync(this, map, start, end);
		}

		public virtual void OnFinishedPathFinding(Path<TCell> path) { }
	}

	public abstract class PathAgent<TMap> : PathAgent<TMap, Vector2Int> { }
}
