using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IGrid2D<T>
	{
		int Width { get; }
		int Height { get; }

		bool InBounds(int x, int y);
		T Get(int x, int y);
		void Set(int x, int y, T value);
		IGrid2D<T> Clone();
	}

	public static class IGrid2DHelper
	{
		public static bool TryGet<T>(this IGrid2D<T> grid, int x, int y, out T result)
		{
			if (grid.InBounds(x, y))
			{
				result = grid.Get(x, y);
				return true;
			}

			result = default(T);
			return true;
		}

		/// <summary>
		/// Counts the number of appearances of each value in this grid
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="grid"></param>
		/// <returns></returns>
		public static Dictionary<T, int> Itemize<T>(this IGrid2D<T> grid)
		{
			var counts = new Dictionary<T, int>();

			for (int x = 0; x < grid.Width; x++)
				for (int y = 0; y < grid.Height; y++)
				{
					var element = grid.Get(x, y);
					if (counts.TryGetValue(element, out int c))
						counts[element] = 1 + c;
					else
						counts[element] = 1;
				}

			return counts;
		}

		/// <summary>
		///
		/// </summary>
		/// <typeparam name="T">Type of the values in the grid</typeparam>
		/// <typeparam name="TResult">Type of the values after operation</typeparam>
		/// <param name="grid"></param>
		/// <param name="function">Function that takes in (x, y, T) and produces TResult</param>
		/// <returns></returns>
		public static Grid2D<TResult> Map<T, TResult>(this IGrid2D<T> grid, System.Func<int, int, T, TResult> function)
		{
			var result = new Grid2D<TResult>(grid.Width, grid.Height);

			for (int x = 0; x < grid.Width; x++)
				for (int y = 0; y < grid.Height; y++)
					result.Set(x, y, function(x, y, grid.Get(x, y)));

			return result;
		}

		public static IEnumerable<KeyValuePair<Vector2Int, T>> GetNeighbors<T>(this IGrid2D<T> grid, Vector2Int pos, int range, bool diagonal)
		{
			for (int x = pos.x - range; x <= pos.x + range; x++)
				for (int y = pos.y - range; y <= pos.y + range; y++)
				{
					if (!diagonal && (x != pos.x && y != pos.y))
						continue;

					var p = new Vector2Int(x, y);
					if (grid.InBounds(x, y) && p != pos)
						yield return new KeyValuePair<Vector2Int, T>(p, grid.Get(x, y));
				}
		}

		/// <summary>
		/// Uses flood search to find all similar connected elements in this grid
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="grid"></param>
		/// <param name="start">Cell to start the search from</param>
		/// <param name="area">Area to constrain search to</param>
		/// <param name="equals">Function that returns true if two values are similar</param>
		/// <returns>List of coordinates</returns>
		public static List<Vector2Int> FloodFindGroup<T>(this IGrid2D<T> grid, Vector2Int start, RectInt area, System.Func<T, T, bool> equals)
		{
			var group = new List<Vector2Int>();
			group.Add(start);

			var toSearch = new Queue<Vector2Int>();
			toSearch.Enqueue(start);

			while (toSearch.TryDequeue(out var cell))
			{
				T v1 = grid.Get(cell.x, cell.y);

				foreach (var neighbor in grid.GetNeighbors(cell, 1, false))
				{
					if (group.Contains(neighbor.Key) || toSearch.Contains(neighbor.Key))
						continue;

					if (equals(v1, grid.Get(neighbor.Key.x, neighbor.Key.y)))
					{
						group.Add(neighbor.Key);
						toSearch.Enqueue(neighbor.Key);
					}
				}
			}

			return group;
		}

		public static List<List<Vector2Int>> FloodFindGroups<T>(this IGrid2D<T> grid, System.Func<T, T, bool> equals)
		{
			var area = new RectInt(0, 0, grid.Width, grid.Height);
			return grid.FloodFindGroups(area, equals);
		}

		/// <summary>
		/// Uses flood search to group every element in this grd
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="grid"></param>
		/// <param name="area">Area to constrain search to</param>
		/// <param name="equals">Function that returns true if two values are similar</param>
		/// <returns>List of list of coordinates</returns>
		public static List<List<Vector2Int>> FloodFindGroups<T>(this IGrid2D<T> grid, RectInt area, System.Func<T, T, bool> equals)
		{
			var grouped = new Grid2D<bool>(area.width, area.height);
			var groups = new List<List<Vector2Int>>();

			for (int x = area.xMin; x < area.xMax; x++)
			{
				for (int y = area.yMin; y < area.yMax; y++)
				{
					if (grouped.Get(x - area.xMin, y - area.yMin))
						continue;

					var group = grid.FloodFindGroup(new Vector2Int(x, y), area, equals);
					groups.Add(group);

					foreach (var cell in group)
						grouped.Set(cell.x - area.xMin, cell.y - area.yMin, true);
				}
			}

			return groups;
		}
	}
}
