using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	public abstract class GridPathAgent<T> : PathAgent<IGrid2D<T>, Vector2Int>
	{
		public bool diagonal = true;

		public override IEnumerable<Vector2Int> GetNeighbors(IGrid2D<T> map, Vector2Int pos)
		{
			for (int xi = -1; xi <= 1; xi++)
			{
				int x = pos.x + xi;

				for (int yi = -1; yi <= 1; yi++)
				{
					if (xi == 0 && yi == 0) continue;

					if (!diagonal && xi != 0 && yi != 0)
						continue;

					int y = pos.y + yi;
					var p = new Vector2Int(x, y);

					if (CanPathThrough(map, p) && map.InBounds(x, y))
						yield return p;
				}
			}
		}

		public override float GetPathCost(IGrid2D<T> map, Vector2Int start, Vector2Int end)
		{
			return Vector2Int.Distance(start, end) * PathThroughCost(map, end);
		}
	}
}
