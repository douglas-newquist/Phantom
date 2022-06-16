using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tiles")]
	public class UniformTilePlacementRule : PlacementRule
	{
		public List<Tile> tiles;

		public override bool CanPlace(TileObjectSO obj, TileObjectMap map, int x, int y)
		{
			if (tiles.Count == 0)
				return false;

			for (int xi = 0; xi < obj.Width; xi++)
			{
				for (int yi = 0; yi < obj.Height; yi++)
				{
					if (!map.InBounds(x + xi, y + yi))
						return false;
					if (!tiles.Contains(map.tiles.Get(x + xi, y + yi)))
						return false;
					if (map.Get(x + xi, y + yi).Item2.Occupied)
						return false;
				}
			}

			return true;
		}
	}
}
