using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public abstract class GridGen : Generator<Grid2D<int>>
	{
		public GridGen mask;

		public override Grid2D<int> Apply(Grid2D<int> grid, RectInt area)
		{
			var result = base.Apply(grid, area);

			if (mask != null)
				return mask.ApplyMask(grid, result, area);

			return result;
		}

		public override Grid2D<int> Apply(Grid2D<int> grid)
		{
			var area = new RectInt(0, 0, grid.Width, grid.Height);
			return Apply(grid, area);
		}

		public override Grid2D<int> Create(int width, int height)
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
