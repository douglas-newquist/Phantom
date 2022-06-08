using System.Collections.Generic;

namespace Phantom
{
	public interface IPathAgent<TMap, TCell>
	{
		IEnumerable<TCell> GetNeighbors(TMap map, TCell pos);

		float GetPathCost(TMap map, TCell start, TCell end);
	}
}
