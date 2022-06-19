using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Fill")]
	public class FillGridGen : GridGen
	{
		public WeightedList<VertexTile> tiles;

		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			grid = new VertexTileMap(grid);

			for (int x = area.xMin; x < area.xMax; x++)
				for (int y = area.yMin; y < area.yMax; y++)
					grid.Set(x, y, tiles.GetRandom());

			return grid;
		}
	}
}
