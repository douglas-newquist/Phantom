using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Applies a tile generator
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Apply Tile Generator")]
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
