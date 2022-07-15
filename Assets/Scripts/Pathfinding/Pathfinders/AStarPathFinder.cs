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

		public override void FindPath<TMap, TCell>(PathRequest<TMap, TCell> request)
		{
			if (request == null) throw new System.ArgumentNullException("request");

			int loop = 0;

			var map = request.Map;
			var agent = request.Agent;

			var searched = new Dictionary<TCell, StarNode<TCell>>();
			var toSearch = new MinHeap<StarNode<TCell>>();

			foreach (var start in request.StartingCells)
			{
				var startNode = new StarNode<TCell>(start)
				{
					HScore = request.GetCheapestGoalCost(start)
				};

				searched.Add(start, startNode);
				toSearch.Insert(startNode);
			}

			PathStatus status = PathStatus.NoPathPossible;
			StarNode<TCell> bestNode = null;

			while (toSearch.TryExtract(out var cell))
			{
				if (loop++ > maxIterations)
				{
					status = PathStatus.TimedOut;
					break;
				}

				if (request.GoalReached(map, cell.Cell))
				{
					status = PathStatus.Found;
					bestNode = cell;
					break;
				}

				if (bestNode == null || cell.HScore < bestNode.HScore)
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
						neighborNode = new StarNode<TCell>(neighbor)
						{
							Previous = cell,
							GScore = tentative,
							HScore = request.GetCheapestGoalCost(neighbor)
						};

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
				request.Path.SetPath(null, status);
			else
				request.Path.SetPath(BuildPath(bestNode), status);

			agent.OnFinishedPathFinding(request.Path);
		}
	}
}
