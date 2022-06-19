using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Fill")]
	public class FillVertexGenerator : VertexGenerator
	{
		[MinMax(1, 16)]
		public IntRange xStep = new IntRange(1, 1);

		[MinMax(1, 16)]
		public IntRange yStep = new IntRange(1, 1);

		public WeightedList<VertexTile> tiles;

		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			grid = new VertexTileMap(grid);

			int stepX = xStep.Random;
			int stepY = yStep.Random;

			for (int x = area.xMin; x < area.xMax; x += stepX)
				for (int y = area.yMin; y < area.yMax; y += stepY)
					grid.TrySet(x, y, tiles.GetRandom());

			return grid;
		}
	}
}
