using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Custom Tile")]
	public class CustomTilePlacementRule : PlacementRule
	{
		public Grid2D<VertexTile> tiles;

		public override bool CanPlace(TileObjectSO obj, TileLayerMap map, Vector3Int position)
		{
			throw new System.NotImplementedException();
		}
	}
}
