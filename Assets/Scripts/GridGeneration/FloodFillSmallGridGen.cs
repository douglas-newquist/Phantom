using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Flood Fill Small")]
	public class FloodFillSmallGridGen : GridGen<int>
	{
		public int areaSize = 4;

		public int value = 1;

		public override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			foreach (var group in grid.FloodFindGroups(area, (v1, v2) => v1 == v2))
			{
				if (group.Count < areaSize && grid.Get(group[0].x, group[0].y) != value)
				{
					Debug.Log(group.Count);
					foreach (var cell in group)
						grid.Set(cell.x, cell.y, value);
				}
			}

			return grid;
		}
	}
}
