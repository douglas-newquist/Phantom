using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Mask")]
	public class MaskGridGen : VertexGenerator
	{
		public int positiveMaskValue = 1;

		public int valueOutsideMask = 0;

		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			var result = new VertexTileMap(grid);
			var gridMask = mask.Create(area.width + 1, area.height + 1);

			for (int x = area.xMin, xMask = 0; x <= area.xMax; x++, xMask++)
			{
				for (int y = area.yMin, yMask = 0; y <= area.yMax; y++, yMask++)
				{
					if (gridMask.Vertices.Get(xMask, yMask) == positiveMaskValue)
						result.Vertices.Set(x, y, grid.Vertices.Get(x, y));
					else
						result.Vertices.Set(x, y, valueOutsideMask);
				}
			}

			return result;
		}
	}
}
