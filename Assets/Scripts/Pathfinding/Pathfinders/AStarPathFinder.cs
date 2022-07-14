using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Phantom.Pathfinding
{
	[CreateAssetMenu(menuName = Pathfinder.CreateMenu + "A*")]
	public partial class AStarPathFinder : Pathfinder
	{
		public bool onlyReturnCompletePath = false;

		public bool reevaluateVisitedOnBetterPath = true;

		protected override void FindPath<TMap, TCell>(IPathAgent<TMap, TCell> agent, TMap map, TCell start, TCell end, Path<TCell> result)
		{
			int loop = 0;

			var searched = new Dictionary<TCell, StarNode<TCell>>();
			var toSearch = new MinHeap<StarNode<TCell>>();

			var startNode = new StarNode<TCell>(null, start, 0, agent.GetPathCost(map, start, end));
			searched.Add(start, startNode);
			toSearch.Insert(startNode);

			PathStatus status = PathStatus.NoPathPossible;
			var bestNode = startNode;

			while (toSearch.TryExtract(out var cell))
			{
				if (loop++ > maxIterations)
				{
					status = PathStatus.TimedOut;
					break;
				}

				if (Equals(cell.Cell, end))
				{
					status = PathStatus.Found;
					bestNode = cell;
					break;
				}

				if (cell.HScore < bestNode.HScore)
					bestNode = cell;

				foreach (var neighbor in agent.GetNeighbors(map, cell.Cell))
				{
					float moveCost = agent.GetPathCost(map, cell.Cell, neighbor);
					float tentative = cell.GScore + moveCost;

					if (cell.Previous != null)
					{
						float subPathCost = agent.GetSubPathExtraCost(map, cell.Previous.Cell, cell.Cell, neighbor);

						if (subPathCost < 0)
							continue;

						tentative += subPathCost;
					}

					if (!searched.TryGetValue(neighbor, out var neighborNode))
					{
						neighborNode = new StarNode<TCell>(cell,
										 neighbor,
										 tentative,
										 agent.GetPathCost(map, neighbor, end));

						searched.Add(neighbor, neighborNode);
						toSearch.Insert(neighborNode);

						agent.OnBetterPathFound(map,
							  default(TCell),
							  cell.Cell,
							  neighbor);
					}
					else if (tentative < neighborNode.GScore)
					{
						if (agent.ChecksCompletePath)
							if (!agent.GetCompletePathPossible(map, BuildPath(neighborNode)))
								continue;

						agent.OnBetterPathFound(map,
							  neighborNode.Previous.Cell,
							  cell.Cell,
							  neighbor);

						neighborNode.GScore = tentative;
						neighborNode.Previous = cell;
						if (reevaluateVisitedOnBetterPath)
							toSearch.Insert(neighborNode);
					}
				}
			}
			if (onlyReturnCompletePath && status != PathStatus.Found)
				result.SetPath(null, status);
			else
				result.SetPath(BuildPath(bestNode), status);

			agent.OnFinishedPathFinding(result);
		}
	}
}
