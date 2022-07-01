using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	[System.Serializable]
	public abstract class Pathfinder : ScriptableObject
	{
		public const string CreateMenu = "Game/Path Finding/Pathfinder/";

		[Range(1, 10000)]
		public int maxIterations = 2000;

		/// <summary>
		/// Stores information about nodes that have been searched
		/// </summary>
		/// <typeparam name="TCell">Coordinate type used in the map</typeparam>
		protected class Node<TCell> : IComparable<Node<TCell>>
		{
			/// <summary>
			/// Best node to reach this one from
			/// </summary>
			public Node<TCell> previous;

			/// <summary>
			/// Where this node is in the map
			/// </summary>
			public TCell pos;

			/// <summary>
			/// Cost of getting to this cell
			/// </summary>
			public float cost;

			public Node(Node<TCell> previous, TCell cell, float cost)
			{
				this.previous = previous;
				this.pos = cell;
				this.cost = cost;
			}

			public int CompareTo(Node<TCell> other)
			{
				return cost.CompareTo(other.cost);
			}
		}

		/// <summary>
		/// Finds a path between two points on the given map
		/// </summary>
		/// <typeparam name="TMap">Type of the map being pathfinded in</typeparam>
		/// <typeparam name="TCell">Type to index individual cells on the map</typeparam>
		/// <param name="agent">Agent pathfinding in the map</param>
		/// <param name="map">Map to pathfind in</param>
		/// <param name="start">Starting position on the map</param>
		/// <param name="end">Ending position on the map</param>
		/// <param name="result">Where to store the resulting path</param>
		protected abstract void FindPath<TMap, TCell>(IPathAgent<TMap, TCell> agent, TMap map, TCell start, TCell end, Path<TCell> result);

		/// <summary>
		/// Finds a path between two points on the given map
		/// </summary>
		/// <typeparam name="TMap">Type of the map being pathfinded in</typeparam>
		/// <typeparam name="TCell">Type to index individual cells on the map</typeparam>
		/// <param name="agent">Agent pathfinding in the map</param>
		/// <param name="map">Map to pathfind in</param>
		/// <param name="start">Starting position on the map</param>
		/// <param name="end">Ending position on the map</param>
		/// <returns></returns>
		public virtual Path<TCell> FindPath<TMap, TCell>(IPathAgent<TMap, TCell> agent, TMap map, TCell start, TCell end)
		{
			var result = new Path<TCell>();
			FindPath(agent, map, start, end, result);
			return result;
		}

		public virtual Path<TCell> FindPathAsync<TMap, TCell>(IPathAgent<TMap, TCell> agent, TMap map, TCell start, TCell end)
		{
			throw new System.NotImplementedException();
		}

		protected virtual List<TCell> BuildPath<TCell>(Node<TCell> end)
		{
			var cells = new List<TCell>();

			while (end.previous != null)
			{
				cells.Add(end.pos);
				end = end.previous;
			}

			cells.Reverse();
			return cells;
		}
	}
}
