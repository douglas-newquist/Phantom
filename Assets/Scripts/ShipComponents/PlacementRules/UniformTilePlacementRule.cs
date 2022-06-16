using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tiles")]
	public class UniformTilePlacementRule : PlacementRule
	{
		public List<Tile> tiles;

		public override bool CanPlace(ShipPartSO part, ShipDesign design, int x, int y)
		{
			if (tiles.Count == 0)
				return false;

			for (int xi = 0; xi < part.width; xi++)
			{
				for (int yi = 0; yi < part.height; yi++)
				{
					if (!design.parts.InBounds(x + xi, y + yi))
						return false;
					if (!tiles.Contains(design.tiles.Get(x + xi, y + yi)))
						return false;
					if (design.parts.Get(x + xi, y + yi).state != Reservation.Free)
						return false;
				}
			}

			return true;
		}
	}
}
