using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Phantom.Pathfinding
{
	[CreateAssetMenu(menuName = CreateMenu.Pathfinder + "A*")]
	public class AStarPathFinder : Pathfinder
	{
		protected class StarNode<T> : Node<T>, IComparable<StarNode<T>>
		{
			public float h;

			public float FScore => cost + h;

			/// <summary>
			///
			/// </summary>
			/// <param name="cell"></param>
			/// <param name="gScore">Actual cost to get here</param>
			/// <param name="hScore">Estimated remaining cost to reach goal</param>
			public StarNode(StarNode<T> previous, T cell, float gScore, float hScore) : base(previous, cell, gScore)
			{
				this.h = hScore;
			}

			public override string ToString()
			{
				var s = pos + "\tG:" + cost + "\tH:" + h + "\tF:" + FScore;
				if (previous != null)
					s += " from " + previous.pos;
				return s;
			}

			public int CompareTo(StarNode<T> other)
			{
				return FScore.CompareTo(other.FScore);
			}
		}

		protected override void FindPath<TMap, TCell>(IPathAgent<TMap, TCell> agent, TMap map, TCell start, TCell end, Path<TCell> result)
		{
			int loop = 0;

			var searched = new Dictionary<TCell, StarNode<TCell>>();
			var toSearch = new MinHeap<StarNode<TCell>>();

			var startNode = new StarNode<TCell>(null, start, 0, agent.GetPathCost(map, start, end));
			searched.Add(start, startNode);
			toSearch.Insert(startNode);

			while (loop++ < maxIterations && toSearch.TryExtract(out var cell))
			{
				if (Equals(cell.pos, end))
				{
					result.SetPath(BuildPath(cell), PathStatus.Found);
					return;
				}

				foreach (var neighbor in agent.GetNeighbors(map, cell.pos))
				{
					float moveCost = agent.GetPathCost(map, cell.pos, neighbor);
					float tentative = cell.cost + moveCost;

					if (!searched.TryGetValue(neighbor, out var neighborNode))
					{
						neighborNode = new StarNode<TCell>(cell,
										 neighbor,
										 tentative,
										 agent.GetPathCost(map, neighbor, end));

						searched.Add(neighbor, neighborNode);
						toSearch.Insert(neighborNode);
					}
					else if (tentative < neighborNode.cost)
					{
						neighborNode.cost = tentative;
						neighborNode.previous = cell;
						toSearch.Insert(neighborNode);
					}
				}
			}

			Debug.Log(loop);
			result.SetPath(null, PathStatus.NoPathPossible);
		}
	}
}
