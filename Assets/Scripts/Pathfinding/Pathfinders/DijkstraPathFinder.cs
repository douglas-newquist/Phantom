using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	[CreateAssetMenu(menuName = Pathfinder.CreateMenu + "Dijkstra")]
	public class DijkstraPathFinder : Pathfinder
	{
		public override void FindPath<TMap, TCell>(PathRequest<TMap, TCell> request)
		{
			if (request == null) throw new System.ArgumentNullException("request");

			int loop = 0;

			var map = request.Map;
			var agent = request.Agent;

			var searched = new Dictionary<TCell, Node<TCell>>();
			var toSearch = new MinHeap<Node<TCell>>();

			foreach (var start in request.StartingCells)
			{
				var startNode = new Node<TCell>(start);
				searched.Add(start, startNode);
				toSearch.Insert(startNode);
			}

			while (loop++ < maxIterations && toSearch.TryExtract(out var cell))
			{
				if (request.GoalReached(map, cell.Cell))
				{
					request.Path.SetPath(BuildPath(cell), PathStatus.Found);
					agent.OnFinishedPathFinding(request.Path);
					return;
				}

				foreach (var neighbor in agent.GetNeighbors(map, cell.Cell))
				{
					float moveCost = agent.GetPathCost(map, cell.Cell, neighbor);
					float tentative = cell.Cost + moveCost;

					if (cell.Previous != null)
					{
						float subPathCost = agent.GetSubPathExtraCost(map, cell.Previous.Cell, cell.Cell, neighbor);

						if (subPathCost < 0)
							continue;

						tentative += subPathCost;
					}

					if (!searched.TryGetValue(neighbor, out var neighborNode))
					{
						agent.OnBetterPathFound(map,
							  default(TCell),
							  cell.Cell,
							  neighbor);

						neighborNode = new Node<TCell>(neighbor)
						{
							Previous = cell,
							Cost = tentative
						};

						searched.Add(neighbor, neighborNode);
						toSearch.Insert(neighborNode);
					}
					else if (tentative < neighborNode.Cost)
					{
						if (agent.ChecksCompletePath)
							if (!agent.GetCompletePathPossible(map, BuildPath(neighborNode)))
								continue;

						agent.OnBetterPathFound(map,
							  neighborNode.Previous.Cell,
							  cell.Cell,
							  neighbor);

						neighborNode.Cost = tentative;
						neighborNode.Previous = cell;
						toSearch.Insert(neighborNode);
					}
				}
			}

			if (loop >= maxIterations)
				request.Path.SetPath(null, PathStatus.TimedOut);
			else
				request.Path.SetPath(null, PathStatus.NoPathPossible);

			agent.OnFinishedPathFinding(request.Path);
		}
	}
}
