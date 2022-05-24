namespace Game
{
	public interface IGrid2D<T>
	{
		int Width { get; }
		int Height { get; }

		bool InBounds(int x, int y);
		T Get(int x, int y);
		void Set(int x, int y, T value);
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
	}
}
