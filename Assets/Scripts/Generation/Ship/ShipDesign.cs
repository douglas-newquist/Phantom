using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class ShipDesign
	{
		public TileMapSO hullType;

		public TileMap tiles;

		public Grid2D<ShipPart> parts;

		public int Width => tiles.Width;

		public int Height => tiles.Height;

		public RectInt BoundingBox => tiles.BoundingBox;

		public ShipDesign(int width, int height)
		{
			tiles = new TileMap(width, height);
			parts = new Grid2D<ShipPart>(width, height);
		}

		public ShipDesign(ShipDesign design)
		{
			tiles = new TileMap(design.tiles);
			parts = new Grid2D<ShipPart>(design.parts);
		}
	}
}
