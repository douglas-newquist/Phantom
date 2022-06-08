using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public abstract class PathAgent<TMap, TCell> : ScriptableObject, IPathAgent<TMap, TCell>
	{
		public Pathfinder pathfinder;

		public abstract bool CanPathThrough(TMap map, TCell pos);

		public abstract IEnumerable<TCell> GetNeighbors(TMap map, TCell pos);

		public abstract float GetPathCost(TMap map, TCell start, TCell end);

		public Path<TCell> FindPath(TMap map, TCell start, TCell end)
		{
			return pathfinder.FindPath(this, map, start, end);
		}

		public Path<TCell> FindPathAsync(TMap map, TCell start, TCell end)
		{
			return pathfinder.FindPathAsync(this, map, start, end);
		}
	}

	public abstract class PathAgent<TMap> : PathAgent<TMap, Vector2Int> { }
}
