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
	}
}
