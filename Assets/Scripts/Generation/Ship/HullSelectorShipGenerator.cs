using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Hull Selector")]
	public class HullSelectorShipGenerator : TileObjectMapGenerator
	{
		public WeightedList<TileMapSO> hulls;

		public override TileObjectMap ApplyOnce(TileObjectMap design, RectInt area)
		{
			var ship = new ShipDesign(design);
			ship.hullType = hulls.GetRandom();
			return ship;
		}
	}
}
