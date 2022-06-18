using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Tiles + "Simple Tile")]
	public class SimpleTile : Tile
	{
		public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			base.StartUp(position, tilemap, go);
			//	go.transform.localPosition = position;
			return true;
		}
	}
}
