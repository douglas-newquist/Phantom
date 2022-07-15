using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom.Pathfinding
{
	[System.Serializable]
	public abstract partial class Pathfinder : ScriptableObject
	{
		public const string CreateMenu = "Game/Path Finding/Pathfinder/";

		[Range(1, 10000)]
		public int maxIterations = 2000;

		/// <summary>
		/// Finds a path between two points on the given map
		/// </summary>
		/// <typeparam name="TMap">Type of the map being pathfinded in</typeparam>
		/// <typeparam name="TCell">Type to index individual cells on the map</typeparam>
		/// <param name="request">Information on path request</param>
		public abstract void FindPath<TMap, TCell>(PathRequest<TMap, TCell> request);

		public virtual Path<TCell> FindPathAsync<TMap, TCell>(PathRequest<TMap, TCell> request)
		{
			throw new System.NotImplementedException();
		}

		protected virtual List<TCell> BuildPath<TCell>(Node<TCell> end)
		{
			var cells = new List<TCell>();

			if (end == null) return cells;

			while (end.Previous != null)
			{
				cells.Add(end.Cell);
				end = end.Previous;
			}

			cells.Reverse();
			return cells;
		}
	}
}
