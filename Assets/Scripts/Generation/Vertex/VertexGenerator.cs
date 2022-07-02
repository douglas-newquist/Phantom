using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public abstract class VertexGenerator : TileLayerMapGenerator
	{
		protected abstract VertexTileMap ApplyOnce(VertexTileMap design, RectInt area);

		public override TileLayerMap ApplyOnce(TileLayerMap design, RectInt area)
		{
			design = new TileLayerMap(design);
			design.VertexTiles = ApplyOnce(design.VertexTiles, area);
			return design;
		}
	}
}
