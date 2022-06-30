using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tiles")]
	public class UniformTilePlacementRule : PlacementRule
	{
		public List<VertexTile> tiles;

		public override bool CanPlace(MapTile tile, TileLayerMap map, Vector3Int position)
		{
			if (tiles.Count == 0)
				return false;

			if (!base.CanPlace(tile, map, position))
				return false;

			for (int x = position.x; x < position.x + tile.Width; x++)
			{
				for (int y = position.y; y < position.y + tile.Height; y++)
				{
					if (!tiles.Contains(map.VertexTiles.Get(x, y)))
						return false;
				}
			}

			return true;
		}
	}
}
