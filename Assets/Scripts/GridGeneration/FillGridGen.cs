using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Fill")]
	public class FillGridGen : GridGen<int>
	{
		public int value = 1;

		public override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			for (int x = area.xMin; x < area.xMax; x++)
				for (int y = area.yMin; x < area.yMax; y++)
					grid.Set(x, y, value);

			return grid;
		}
	}
}
