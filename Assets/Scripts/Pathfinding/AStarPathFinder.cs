using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Pathfinder + "A*")]
	public class AStarPathFinder : Pathfinder
	{
		protected override void FindPath<TMap, TCell>(IPathAgent<TMap, TCell> agent, TMap map, TCell start, TCell end, Path<TCell> result)
		{
			throw new System.NotImplementedException();
		}
	}
}
