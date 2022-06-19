using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tile Shapes")]
	public class UniformShapePlacementRule : PlacementRule
	{
		public VertexTileShape shape;

		public override bool CanPlace(MapTile tile, TileLayerMap map, Vector3Int position)
		{
			if (!base.CanPlace(tile, map, position))
				return false;

			for (int x = position.x; x < position.x + tile.Width; x++)
			{
				for (int y = position.y; y < position.y + tile.Height; y++)
				{
					if (map.Tiles.TryGet(x, y, out var t))
						if (!shape.HasFlag(t.Shape()))
							return false;
				}
			}

			return true;
		}
	}
}
