using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Mask")]
	public class MaskGridGen : GridGen<int>
	{
		public GridGen<int> mask;

		public int positiveMaskValue = 1;

		public int valueOutsideMask = 0;

		public override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			var result = new Grid2D<int>(grid);
			var gridMask = mask.Create(area.width, area.height);

			for (int x = area.xMin, xMask = 0; x < area.xMax; x++, xMask++)
			{
				for (int y = area.yMin, yMask = 0; y < area.yMax; y++, yMask++)
				{
					if (gridMask.Get(xMask, yMask) == positiveMaskValue)
						result.Set(x, y, grid.Get(x, y));
					else
						result.Set(x, y, valueOutsideMask);
				}
			}

			return result;
		}
	}
}
