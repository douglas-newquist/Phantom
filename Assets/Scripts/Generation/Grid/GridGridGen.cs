using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Generators/Tiles/Grid")]
	public class GridGridGen : GridGen
	{
		[MinMax(1, 64)]
		public IntRange x = new IntRange(2, 4), y = new IntRange(2, 4);

		[MinMax(-1, 64)]
		public IntRange xSpacing = new IntRange(1, 1);

		[MinMax(-1, 64)]
		public IntRange ySpacing = new IntRange(1, 1);

		public Slice slice;

		public enum Slice
		{
			CellCount,
			CellSize
		}

		public GridGen cell;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			int xStep = 1, yStep = 1;
			int xOffset = 0, yOffset = 0;
			int xSpace = xSpacing.Random;
			int ySpace = ySpacing.Random;

			switch (slice)
			{
				case Slice.CellSize:
					xStep = x.Random;
					yStep = y.Random;
					break;

				case Slice.CellCount:
					int xCells = x.Random;
					int yCells = y.Random;
					int width = area.width - xSpace * (xCells - 1);
					int height = area.height - ySpace * (yCells - 1);
					xStep = width / xCells;
					xOffset = (width % xCells) / 2;
					yStep = height / yCells;
					yOffset = (height % yCells) / 2;
					break;
			}

			for (int x = area.xMin + xOffset; x < area.xMax; x += xStep + xSpace)
			{
				for (int y = area.yMin + yOffset; y < area.yMax; y += yStep + ySpace)
				{
					RectInt subArea = new RectInt(x, y, xStep, yStep);
					subArea.xMax = Mathf.Clamp(subArea.xMax, area.xMin, area.xMax - 1);
					subArea.yMax = Mathf.Clamp(subArea.yMax, area.yMin, area.yMax - 1);
					grid = cell.Apply(grid, subArea);
				}
			}

			return grid;
		}
	}
}
