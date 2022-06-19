using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tile Shapes")]
	public class UniformShapePlacementRule : PlacementRule
	{
		public VertexTileShape shape;

		public override bool CanPlace(MapTile obj, TileLayerMap map, Vector3Int position)
		{
			for (int xi = 0; xi < obj.Width; xi++)
			{
				for (int yi = 0; yi < obj.Height; yi++)
				{
					if (map.Tiles.TryGet(position.x + xi, position.y + yi, out var tile))
						if (!shape.HasFlag(tile.Shape()))
							return false;
				}
			}

			return true;
		}
	}
}
