using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	public abstract class GridPathAgent<T> : PathAgent<IGrid2D<T>, Vector2Int>
	{
		public DiagonalMode diagonal = DiagonalMode.Allow;

		public DistanceMode distanceMode = DistanceMode.SquareRoot;

		public bool simplifyPath = false;

		[Range(-1, 16)]
		public float outOfBoundsCost = -1;

		[Range(0, 16)]
		public float directionChangedCost = 0;

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
								if (PathThroughCost(map, new Vector2Int(pos.x, y)) < 0)
									continue;
								if (PathThroughCost(map, new Vector2Int(x, pos.y)) < 0)
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
			float dist = 0;

			switch (distanceMode)
			{
				case DistanceMode.Manhattan:
					dist += Mathf.Abs(start.x - end.x);
					dist += Mathf.Abs(start.y - end.y);
					break;

				case DistanceMode.Square:
					dist = ((Vector2)(end - start)).sqrMagnitude;
					break;

				case DistanceMode.SquareRoot:
					dist = Vector2Int.Distance(start, end);
					break;
			}

			return dist * PathThroughCost(map, end);
		}

		public override float GetSubPathExtraCost(IGrid2D<T> map, Vector2Int a, Vector2Int b, Vector2Int c)
		{
			Vector2 d1 = b - a;
			Vector2 d2 = c - b;

			if (Vector2.Angle(d1.normalized, d2.normalized) < 5)
				return 0;

			return directionChangedCost;
		}

		public override void OnFinishedPathFinding(Path<Vector2Int> path)
		{
			if (!simplifyPath) return;
			if (path.Length < 3) return;

			for (int i = path.Length - 2; i >= 1; i--)
			{
				Vector2 d1 = path[i] - path[i - 1];
				Vector2 d2 = path[i + 1] - path[i];

				if (Vector2.Angle(d1.normalized, d2.normalized) < 5)
					path.RemoveAt(i);
			}
		}
	}
}
