using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Marching Squares")]
	public class MarchingSquaresGridGen : GridGen<int>
	{
		public IntRange n;

		public int alive = 1, dead = 0;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			var result = new Grid2D<int>(grid);

			for (int x = area.xMin; x < area.xMax; x++)
				for (int y = area.yMin; y < area.yMax; y++)
				{
					int active = 0;
					foreach (var neighbor in grid.GetNeighbors(new Vector2Int(x, y), 1, true))
						if (neighbor.Value == alive)
							active++;

					if (active <= n.Min)
						result.Set(x, y, dead);
					else if (active > n.Max)
						result.Set(x, y, alive);
				}

			return result;
		}
	}
}
