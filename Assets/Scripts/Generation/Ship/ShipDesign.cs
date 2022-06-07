using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class ShipDesign
	{
		public TileMap tiles = new TileMap(64, 64);

		public Grid2D<ShipPart> parts = new Grid2D<ShipPart>(64, 64);

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
