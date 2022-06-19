using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public abstract class GridGen : Generator<VertexTileMap>
	{
		public enum Mode
		{
			Vertices,
			Tiles
		}

		public GridGen mask;

		public override VertexTileMap Apply(VertexTileMap grid, RectInt area)
		{
			var result = base.Apply(grid, area);

			if (mask != null)
				return mask.ApplyMask(grid, result, area);

			return result;
		}

		public override VertexTileMap Apply(VertexTileMap grid)
		{
			var area = new RectInt(0, 0, grid.Width, grid.Height);
			return Apply(grid, area);
		}

		public override VertexTileMap Create(int width, int height)
		{
			return Apply(new VertexTileMap(width, height));
		}

		public virtual VertexTileMap ApplyMask(VertexTileMap negative, VertexTileMap positive, RectInt area)
		{
			var result = new VertexTileMap(negative);

			var gridMask = Create(area.width, area.height);

			for (int x = area.xMin, xMask = 0; x < area.xMax; x++, xMask++)
				for (int y = area.yMin, yMask = 0; y < area.yMax; y++, yMask++)
					if (gridMask.Vertices.Get(xMask, yMask) == 1)
						result.Vertices.Set(x, y, positive.Vertices.Get(x, y));

			return result;
		}
	}
}
