using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PathAgent + "Tile Map Agent")]
	public class TilePathAgent : GridPathAgent<TileMap>
	{
		public TileWeights weights;

		public override bool CanPathThrough(IGrid2D<TileMap> map, Vector2Int pos)
		{
			throw new System.NotImplementedException();
		}
	}
}
