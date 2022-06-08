using UnityEngine;

namespace Phantom.Pathfinding
{
	[CreateAssetMenu(menuName = CreateMenu.PathAgent + "Tile Map Agent")]
	public class TilePathAgent : GridPathAgent<Tile>
	{
		public TileWeights weights;

		public override float PathThroughCost(IGrid2D<Tile> map, Vector2Int pos)
		{
			return weights.GetWeight(map.Get(pos.x, pos.y));
		}
	}
}
