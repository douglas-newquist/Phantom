using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Tiles + "Simple Tile")]
	public class SimpleTile : Tile
	{
		public Sprite[] sprites;

		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			tileData.color = color;
			tileData.colliderType = colliderType;
			tileData.gameObject = gameObject;
			tileData.flags = flags;
			tileData.transform = transform;

			if (sprites.Length > 0)
				tileData.sprite = sprites[Random.Range(0, sprites.Length)];
			else
				tileData.sprite = sprite;
		}
	}
}
