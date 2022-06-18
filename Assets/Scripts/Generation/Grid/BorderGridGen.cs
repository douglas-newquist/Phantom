using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Border")]
	public class BorderGridGen : GridGen
	{
		[MinMax(0, 32)]
		public IntRange topBorder = new IntRange(3, 3);

		[MinMax(0, 32)]
		public IntRange bottomBorder = new IntRange(3, 3);

		[MinMax(0, 32)]
		public IntRange leftBorder = new IntRange(3, 3);

		[MinMax(0, 32)]
		public IntRange rightBorder = new IntRange(3, 3);

		public int value = 1;

		public override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			TopBorder(grid, area);
			BottomBorder(grid, area);
			LeftBorder(grid, area);
			RightBorder(grid, area);

			return grid;
		}

		public void TopBorder(Grid2D<int> grid, RectInt area)
		{
			for (int x = area.xMin; x < area.xMax; x++)
			{
				int depth = topBorder.Random;

				for (int yi = 0; yi < depth; yi++)
					if (grid.InBounds(x, area.yMin + yi))
						grid.Set(x, area.yMin + yi, value);
			}
		}

		public void BottomBorder(Grid2D<int> grid, RectInt area)
		{
			for (int x = area.xMin; x < area.xMax; x++)
			{
				int depth = topBorder.Random;

				for (int yi = 0; yi < depth; yi++)
					if (grid.InBounds(x, area.yMax - yi - 1))
						grid.Set(x, area.yMax - yi - 1, value);
			}
		}

		public void LeftBorder(Grid2D<int> grid, RectInt area)
		{
			for (int y = area.yMin; y < area.yMax; y++)
			{
				int depth = leftBorder.Random;

				for (int xi = 0; xi < depth; xi++)
					if (grid.InBounds(area.xMin + xi, y))
						grid.Set(area.xMin + xi, y, value);
			}
		}

		public void RightBorder(Grid2D<int> grid, RectInt area)
		{
			for (int y = area.yMin; y < area.yMax; y++)
			{
				int depth = rightBorder.Random;

				for (int xi = 0; xi < depth; xi++)
					if (grid.InBounds(area.xMax - xi - 1, y))
						grid.Set(area.xMax - xi - 1, y, value);
			}
		}
	}
}
