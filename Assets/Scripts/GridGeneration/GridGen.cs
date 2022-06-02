using UnityEngine;

namespace Game
{
	[System.Serializable]
	public abstract class GridGen<T> : ScriptableObject
	{
		public IntRange repeat = new IntRange(1, 1);

		public abstract Grid2D<T> Apply(Grid2D<T> grid, RectInt area);

		public virtual Grid2D<T> Apply(Grid2D<T> grid)
		{
			int repeats = repeat.Random;
			var area = new RectInt(0, 0, grid.Width, grid.Height);

			for (int i = 0; i < repeats; i++)
				grid = Apply(grid, area);

			return grid;
		}

		public virtual Grid2D<T> Create(int width, int height)
		{
			return Apply(new Grid2D<T>(width, height), new RectInt(0, 0, width, height));
		}
	}
}
