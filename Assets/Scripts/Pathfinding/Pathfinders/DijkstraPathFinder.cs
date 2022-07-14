using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	[CreateAssetMenu(menuName = Pathfinder.CreateMenu + "Dijkstra")]
	public class DijkstraPathFinder : Pathfinder
	{
		protected override void FindPath<TMap, TCell>(IPathAgent<TMap, TCell> agent, TMap map, TCell start, TCell end, Path<TCell> result)
		{
			int loop = 0;

			var searched = new Dictionary<TCell, Node<TCell>>();
			var toSearch = new MinHeap<Node<TCell>>();

			var startNode = new Node<TCell>(null, start, 0);
			searched.Add(start, startNode);
			toSearch.Insert(startNode);

			while (loop++ < maxIterations && toSearch.TryExtract(out var cell))
			{
				if (Equals(cell.pos, end))
				{
					result.SetPath(BuildPath(cell), PathStatus.Found);
					agent.OnFinishedPathFinding(result);
					return;
				}

				foreach (var neighbor in agent.GetNeighbors(map, cell.pos))
				{
					float moveCost = agent.GetPathCost(map, cell.pos, neighbor);
					float tentative = cell.cost + moveCost;

					if (cell.previous != null)
					{
						float subPathCost = agent.GetSubPathExtraCost(map, cell.previous.pos, cell.pos, neighbor);

						if (subPathCost < 0)
							continue;

						tentative += subPathCost;
					}

					if (!searched.TryGetValue(neighbor, out var neighborNode))
					{
						agent.OnBetterPathFound(map,
							  default(TCell),
							  cell.pos,
							  neighbor);

						neighborNode = new Node<TCell>(cell, neighbor, tentative);
						searched.Add(neighbor, neighborNode);
						toSearch.Insert(neighborNode);
					}
					else if (tentative < neighborNode.cost)
					{
						agent.OnBetterPathFound(map,
							  neighborNode.previous.pos,
							  cell.pos,
							  neighbor);

						neighborNode.cost = tentative;
						neighborNode.previous = cell;
						toSearch.Insert(neighborNode);
					}
				}
			}

			if (loop >= maxIterations)
				result.SetPath(null, PathStatus.TimedOut);
			else
				result.SetPath(null, PathStatus.NoPathPossible);

			agent.OnFinishedPathFinding(result);
		}
	}
}
