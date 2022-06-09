using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	[CreateAssetMenu(menuName = CreateMenu.Pathfinder + "Dijkstra")]
	public class DijkstraPathFinder : Pathfinder
	{
		protected override void FindPath<TMap, TCell>(IPathAgent<TMap, TCell> agent, TMap map, TCell start, TCell end, Path<TCell> result)
		{
			int loop = 0;

			var searched = new Dictionary<TCell, Node<TCell>>();
			var toSearch = new Queue<Node<TCell>>();

			var startNode = new Node<TCell>(null, start, 0);
			searched.Add(start, startNode);
			toSearch.Enqueue(startNode);

			while (loop++ < maxIterations && toSearch.TryDequeue(out var cell))
			{
				if (Equals(cell.cell, end))
				{
					result.SetPath(BuildPath(cell), PathStatus.Found);
					return;
				}

				foreach (var neighbor in agent.GetNeighbors(map, cell.cell))
				{
					float moveCost = agent.GetPathCost(map, cell.cell, neighbor);
					float tentative = cell.cost + moveCost;

					if (!searched.TryGetValue(neighbor, out var neighborNode))
					{
						neighborNode = new Node<TCell>(cell, neighbor, tentative);
						searched.Add(neighbor, neighborNode);
						toSearch.Enqueue(neighborNode);
					}
					else if (cell.cost + moveCost < neighborNode.cost)
					{
						neighborNode.cost = tentative;
						neighborNode.previous = cell;
						toSearch.Enqueue(neighborNode);
					}
				}
			}

			result.SetPath(null, PathStatus.NoPathPossible);
		}
	}
}
