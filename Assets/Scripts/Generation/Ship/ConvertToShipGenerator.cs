using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Tile Object Map to Ship")]
	public class ConvertToShipGenerator : TileObjectMapGenerator
	{
		public override TileObjectMap ApplyOnce(TileObjectMap design, RectInt area)
		{
			return new ShipDesign(design);
		}

		public override TileObjectMap Create(int width, int height)
		{
			var map = new ShipDesign(width, height);
			if (map != null)
				map.Tiles.Vertices = tileGenerator.Apply(map.Tiles.Vertices);
			return map;
		}
	}
}
