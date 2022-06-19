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

		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			grid = new VertexTileMap(grid);

			TopBorder(grid, area);
			BottomBorder(grid, area);
			LeftBorder(grid, area);
			RightBorder(grid, area);

			return grid;
		}

		public void BottomBorder(VertexTileMap grid, RectInt area)
		{
			for (int x = area.xMin; x <= area.xMax; x++)
			{
				int depth = bottomBorder.Random;

				for (int yi = 0; yi < depth; yi++)
					grid.Vertices.TrySet(x, area.yMin + yi, value);
			}
		}

		public void TopBorder(VertexTileMap grid, RectInt area)
		{
			for (int x = area.xMin; x <= area.xMax; x++)
			{
				int depth = topBorder.Random;

				for (int yi = 0; yi < depth; yi++)
					grid.Vertices.TrySet(x, area.yMax - yi, value);
			}
		}

		public void LeftBorder(VertexTileMap grid, RectInt area)
		{
			for (int y = area.yMin; y <= area.yMax; y++)
			{
				int depth = leftBorder.Random;

				for (int xi = 0; xi < depth; xi++)
					grid.Vertices.TrySet(area.xMin + xi, y, value);
			}
		}

		public void RightBorder(VertexTileMap grid, RectInt area)
		{
			for (int y = area.yMin; y <= area.yMax; y++)
			{
				int depth = rightBorder.Random;

				for (int xi = 0; xi < depth; xi++)
					grid.Vertices.TrySet(area.xMax - xi, y, value);
			}
		}
	}
}
