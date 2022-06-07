using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tile Shapes")]
	public class UniformShapePlacementRule : PlacementRule
	{
		public TileShape shape;

		public override bool CanPlace(ShipPartSO part, ShipDesign design, int x, int y)
		{
			return false;
			throw new System.NotImplementedException();
		}
	}
}
