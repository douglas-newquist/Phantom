using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	public abstract class PathAgent<TMap, TCell> : ScriptableObject, IPathAgent<TMap, TCell>
	{
		public const string CreateMenu = "Game/Path Finding/Agent/";

		public Pathfinder pathfinder;

		public bool ChecksCompletePath => false;

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

		public PathRequest<TMap, TCell> FindPath(PathRequest<TMap, TCell> request)
		{
			pathfinder.FindPath(request);
			return request;
		}

		public PathRequest<TMap, TCell> FindPathAsync(PathRequest<TMap, TCell> request)
		{
			pathfinder.FindPathAsync(request);
			return request;
		}

		public abstract IEnumerable<TCell> GetNeighbors(TMap map, TCell pos);

		public abstract float GetPathCost(TMap map, TCell start, TCell end);

		public virtual void OnFinishedPathFinding(Path<TCell> path) { }

		public virtual float GetSubPathExtraCost(TMap map, TCell a, TCell b, TCell c) => 0;

		public virtual void OnBetterPathFound(TMap map, TCell original, TCell current, TCell end)
		{ }

		public virtual bool GetCompletePathPossible(TMap map, IEnumerable<TCell> path)
		{
			throw new System.NotImplementedException();
		}
	}

	public abstract class PathAgent<TMap> : PathAgent<TMap, Vector2Int> { }
}
