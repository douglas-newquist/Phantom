namespace Game
{
	[System.Serializable]
	public class ShipDesign
	{
		public TileMap tiles = new TileMap(64, 64);
		public Grid2D<ShipPart> parts = new Grid2D<ShipPart>(64, 64);

		public ShipDesign(int width, int height)
		{
			tiles = new TileMap(width, height);
			parts = new Grid2D<ShipPart>(width, height);
		}
	}
}
