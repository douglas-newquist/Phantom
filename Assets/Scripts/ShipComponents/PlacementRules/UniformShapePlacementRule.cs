using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tile Shapes")]
	public class UniformShapePlacementRule : PlacementRule
	{
		public TileShape shape;

		public override bool CanPlace(ShipPartSO part, ShipDesign design, int x, int y)
		{
			for (int xi = 0; xi < part.width; xi++)
			{
				for (int yi = 0; yi < part.height; yi++)
				{
					if (!design.parts.InBounds(x + xi, y + yi))
						return false;
					if (design.parts.Get(x + xi, y + yi).state != SlotState.Free)
						return false;
					if (!shape.HasFlag(design.tiles.Get(x + xi, y + yi).Shape()))
						return false;
				}
			}

			return true;
		}
	}
}
