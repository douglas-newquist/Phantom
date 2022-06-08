using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IPathAgent<TMap, TCell>
	{
		IEnumerable<TCell> GetNeighbors(TMap map, TCell pos);

		float GetPathCost(TMap map, TCell start, TCell end);
	}

	public interface IPathAgent<TMap> : IPathAgent<TMap, Vector2Int> { }
}
