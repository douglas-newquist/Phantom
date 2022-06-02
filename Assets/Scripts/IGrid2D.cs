using System.Collections.Generic;
using UnityEngine;

namespace Game
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

		public static IEnumerable<KeyValuePair<Vector2Int, T>> GetNeighbors<T>(this IGrid2D<T> grid, Vector2Int pos, int range)
		{
			for (int x = pos.x - range; x <= pos.x + range; x++)
				for (int y = pos.y - range; y <= pos.y + range; y++)
				{
					var p = new Vector2Int(x, y);
					if (grid.InBounds(x, y) && p != pos)
						yield return new KeyValuePair<Vector2Int, T>(p, grid.Get(x, y));
				}
		}
	}
}
