using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	public abstract class GridPathAgent<T> : PathAgent<IGrid2D<T>, Vector2Int>
	{
		public DiagonalMode diagonal = DiagonalMode.Allow;

		[Range(-1, 16)]
		public float outOfBoundsCost = -1;

		public override IEnumerable<Vector2Int> GetNeighbors(IGrid2D<T> map, Vector2Int pos)
		{
			for (int xi = -1; xi <= 1; xi++)
			{
				int x = pos.x + xi;

				for (int yi = -1; yi <= 1; yi++)
				{
					int y = pos.y + yi;
					var p = new Vector2Int(x, y);

					if (xi == 0 && yi == 0) continue;

					if (xi != 0 && yi != 0)
					{
						switch (diagonal)
						{
							case DiagonalMode.Disallow:
								continue;

							case DiagonalMode.AllowIfBothOrthogonal:
								if (PathThroughCost(map, new Vector2Int(0, y)) < 0)
									continue;
								if (PathThroughCost(map, new Vector2Int(x, 0)) < 0)
									continue;
								break;
						}
					}

					if (PathThroughCost(map, p) >= 0)
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
