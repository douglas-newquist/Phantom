using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tiles")]
	public class UniformTilePlacementRule : PlacementRule
	{
		public List<VertexTile> tiles;

		public override bool CanPlace(TileObjectSO obj, TileLayerMap map, Vector3Int position)
		{
			if (tiles.Count == 0)
				return false;

			for (int xi = 0; xi < obj.Width; xi++)
			{
				for (int yi = 0; yi < obj.Height; yi++)
				{
					if (!map.InBounds(position.x + xi, position.y + yi))
						return false;
					if (!tiles.Contains(map.Tiles.Get(position.x + xi, position.y + yi)))
						return false;
				}
			}

			return true;
		}
	}
}
