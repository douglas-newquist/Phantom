using UnityEngine;

namespace Phantom.Pathfinding
{
	[CreateAssetMenu(menuName = CreateMenu.PathAgent + "Tile Map Agent")]
	public class TilePathAgent : GridPathAgent<VertexTile>
	{
		public VertexTileShapePair<float> weights;

		public override float PathThroughCost(IGrid2D<VertexTile> map, Vector2Int pos)
		{
			if (!map.InBounds(pos.x, pos.y))
				return outOfBoundsCost;

			var tile = map.Get(pos.x, pos.y);
			return weights.Get(tile);
		}
	}
}
