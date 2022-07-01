using UnityEngine;

namespace Phantom.Pathfinding
{
	[CreateAssetMenu(menuName = CreateMenu + "Vertex Agent")]
	public class VertexPathAgent : GridPathAgent<int>
	{
		[Range(-1, 16)]
		public float activeCost = -1;

		[Range(-1, 16)]
		public float inactiveCost = 1;

		public override float PathThroughCost(IGrid2D<int> map, Vector2Int pos)
		{
			if (!map.InBounds(pos.x, pos.y))
				return outOfBoundsCost;

			return map.Get(pos.x, pos.y) == 0 ? inactiveCost : activeCost;
		}
	}
}
