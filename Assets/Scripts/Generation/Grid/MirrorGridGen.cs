using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Mirror")]
	public class MirrorGridGen : GridGen
	{
		public Mirror mirror;

		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			grid = new VertexTileMap(grid);

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

		public void MirrorX(VertexTileMap grid, RectInt area)
		{
			for (int x = area.xMin; x <= area.xMax; x++)
			{
				int y1 = area.yMin;
				int y2 = area.yMax;

				for (; y1 < y2; y1++, y2--)
					if (grid.Vertices.TryGet(x, y2, out var vertex))
						grid.Vertices.Set(x, y2, vertex);
			}
		}

		public void MirrorY(VertexTileMap grid, RectInt area)
		{
			for (int y = area.yMin; y <= area.yMax; y++)
			{
				int x1 = area.xMin;
				int x2 = area.xMax;

				for (; x1 < x2; x1++, x2--)
					if (grid.Vertices.TryGet(x1, y, out var vertex))
						grid.Vertices.Set(x2, y, vertex);
			}
		}
	}
}
