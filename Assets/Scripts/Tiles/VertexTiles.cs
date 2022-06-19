using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Tiles + "Vertex Tiles")]
	public class VertexTiles : ScriptableObject
	{
		public VertexTilePair<VertexTileTile> tiles;

		public VertexTileTile GetTile(VertexTile tile)
		{
			return tiles.Get(tile);
		}
	}
}
