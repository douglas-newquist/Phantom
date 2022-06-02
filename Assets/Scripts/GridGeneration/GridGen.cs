using UnityEngine;

namespace Game
{
	[System.Serializable]
	public abstract class GridGen : ScriptableObject
	{
		public IntRange repeat = new IntRange(1, 1);

		public GridGen mask;

		protected abstract Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area);

		public virtual Grid2D<int> Apply(Grid2D<int> grid, RectInt area)
		{
			var result = new Grid2D<int>(grid);
			int repeats = repeat.Random;

			for (int i = 0; i < repeats; i++)
				result = ApplyOnce(result, area);

			if (mask != null)
				return mask.ApplyMask(grid, result, area);

			return result;
		}

		public virtual Grid2D<int> Apply(Grid2D<int> grid)
		{
			var area = new RectInt(0, 0, grid.Width, grid.Height);
			return Apply(grid, area);
		}

		public virtual Grid2D<int> Create(int width, int height)
		{
			return Apply(new Grid2D<int>(width, height));
		}

		public virtual Grid2D<int> ApplyMask(Grid2D<int> negative, Grid2D<int> positive, RectInt area)
		{
			var result = new Grid2D<int>(negative);

			var gridMask = Create(area.width, area.height);

			for (int x = area.xMin, xMask = 0; x < area.xMax; x++, xMask++)
				for (int y = area.yMin, yMask = 0; y < area.yMax; y++, yMask++)
					if (gridMask.Get(xMask, yMask) == 1)
						result.Set(x, y, positive.Get(x, y));

			return result;
		}
	}
}
