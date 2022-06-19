using UnityEngine;

namespace Phantom
{
	public abstract class TileLayerMapGenerator : Generator<TileLayerMap>
	{
		public VertexGenerator tileGenerator;

		public override TileLayerMap Apply(TileLayerMap design)
		{
			return Apply(design, new RectInt(0, 0, design.Width, design.Height));
		}

		public override TileLayerMap Create(int width, int height)
		{
			var map = new TileLayerMap(width, height);
			if (map != null)
				map.Tiles = tileGenerator.Apply(map.Tiles);
			return Apply(map);
		}
	}
}
