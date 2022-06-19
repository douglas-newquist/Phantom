using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Invert")]
	public class InvertVertexGenerator : VertexGenerator
	{
		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			grid = new VertexTileMap(grid);

			for (int x = area.xMin; x <= area.xMax; x++)
				for (int y = area.yMin; y <= area.yMax; y++)
					if (grid.Vertices.Get(x, y) == 0)
						grid.Vertices.Set(x, y, 1);
					else
						grid.Vertices.Set(x, y, 0);

			return grid;
		}
	}
}
