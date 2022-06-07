using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Generators/Ships/Apply Tile Generator")]
	public class ShipApplyTileGenerator : ShipGenerator
	{
		public GridGen tileGenerator;

		public override ShipDesign ApplyOnce(ShipDesign design, RectInt area)
		{
			design = new ShipDesign(design);
			design.tiles.vertices = tileGenerator.Apply(design.tiles.vertices, area);
			return design;
		}
	}
}
