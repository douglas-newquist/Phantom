using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Border")]
	public class BorderGridGen : GridGen
	{
		public IntRange topBottom = new IntRange(3, 3);

		public IntRange leftRight = new IntRange(3, 3);

		public int value = 1;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			for (int y = area.yMin; y < area.yMax; y++)
			{
				int depth = leftRight.Random;

				for (int x = 0; x < depth; x++)
					grid.Set(x + area.xMin, y, value);

				depth = leftRight.Random;

				for (int x = 0; x < depth; x++)
					grid.Set(area.xMax - x - 1, y, value);
			}

			for (int x = area.xMin; x < area.xMax; x++)
			{
				int depth = topBottom.Random;

				for (int y = 0; y < depth; y++)
					grid.Set(x, y + area.yMin, value);

				depth = topBottom.Random;

				for (int y = 0; y < depth; y++)
					grid.Set(x, area.yMax - y - 1, value);
			}

			return grid;
		}
	}
}
