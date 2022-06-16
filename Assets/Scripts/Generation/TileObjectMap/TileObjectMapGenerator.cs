using UnityEngine;

namespace Phantom
{
	public abstract class TileObjectMapGenerator : Generator<TileObjectMap>
	{
		public GridGen tileGenerator;

		public override TileObjectMap Apply(TileObjectMap design)
		{
			return Apply(design, new RectInt(0, 0, design.Width, design.Height));
		}

		public override TileObjectMap Create(int width, int height)
		{
			var map = new TileObjectMap(width, height);
			if (map != null)
				map.tiles.vertices = tileGenerator.Apply(map.tiles.vertices);
			return Apply(map);
		}
	}
}
