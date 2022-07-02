using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Tiles + "Simple Tile")]
	public class SimpleTile : Tile
	{
		public Sprite[] sprites;
	}
}
