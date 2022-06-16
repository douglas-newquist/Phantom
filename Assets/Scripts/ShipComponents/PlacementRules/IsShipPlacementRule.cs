using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Is Ship", fileName = "Is Ship")]
	public class IsShipPlacementRule : PlacementRule
	{
		public override bool CanPlace(TileObjectSO obj, TileObjectMap map, int x, int y)
		{
			return map is ShipDesign;
		}
	}
}
