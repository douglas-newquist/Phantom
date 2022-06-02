using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Mirror")]
	public class MirrorGridGen : GridGen<int>
	{
		public Mirror mirror;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			switch (mirror)
			{
				case Mirror.X:
					MirrorX(grid, area);
					break;

				case Mirror.Y:
					MirrorY(grid, area);
					break;

				case Mirror.XY:
					MirrorX(grid, area);
					MirrorY(grid, area);
					break;

				case Mirror.YX:
					MirrorY(grid, area);
					MirrorX(grid, area);
					break;
			}

			return grid;
		}

		public void MirrorX(Grid2D<int> grid, RectInt area)
		{
			for (int x = area.xMin; x < area.xMax; x++)
			{
				int y1 = area.yMin;
				int y2 = area.yMax - 1;

				for (; y1 < y2; y1++, y2--)
					grid.Set(x, y2, grid.Get(x, y1));
			}
		}

		public void MirrorY(Grid2D<int> grid, RectInt area)
		{
			for (int y = area.yMin; y < area.yMax; y++)
			{
				int x1 = area.xMin;
				int x2 = area.xMax - 1;

				for (; x1 < x2; x1++, x2--)
					grid.Set(x2, y, grid.Get(x1, y));
			}
		}
	}
}
