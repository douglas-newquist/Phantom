using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Custom Tile")]
	public class CustomTilePlacementRule : PlacementRule
	{
		public Grid2D<Tile> tiles;

		public override bool CanPlace(ShipPartSO part, ShipDesign design, int x, int y)
		{
			throw new System.NotImplementedException();
		}
	}
}
