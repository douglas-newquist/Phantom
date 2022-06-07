using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Hull Selector")]
	public class HullSelectorShipGenerator : ShipGenerator
	{
		public WeightedList<TileMapSO> hulls;

		public override ShipDesign ApplyOnce(ShipDesign design, RectInt area)
		{
			design = new ShipDesign(design);
			design.hullType = hulls.GetRandom();
			return design;
		}
	}
}
