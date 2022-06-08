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
			searched.Add(start, new Node<TCell>(start, 0));

			var toSearch = new Queue<TCell>();
			toSearch.Enqueue(start);

			while (loop++ < maxIterations && toSearch.Count > 0)
			{
				TCell cell = toSearch.Dequeue();
				var cellNode = searched[cell];

				if (Equals(cell, end))
				{
					result.SetPath(BuildPath(cellNode), PathStatus.Found);
					return;
				}

				foreach (var neighbor in agent.GetNeighbors(map, cell))
				{
					if (!searched.TryGetValue(neighbor, out var neighborNode))
					{
						neighborNode = new Node<TCell>(neighbor, float.MaxValue);
						searched.Add(neighbor, neighborNode);
					}

					float moveCost = agent.GetPathCost(map, cell, neighbor);

					if (cellNode.cost + moveCost < neighborNode.cost)
					{
						neighborNode.cost = cellNode.cost + moveCost;
						neighborNode.previous = cellNode;
					}
				}
			}

			result.SetPath(null, PathStatus.NoPathPossible);
		}
	}
}
