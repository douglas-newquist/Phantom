using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Tiles + "Variable Tile")]
	public class VariableTile : Tile
	{
		public WeightedList<Sprite> sprites;

		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			base.GetTileData(position, tilemap, ref tileData);
			tileData.sprite = sprites.GetRandom();
		}
	}
}
