using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Invert")]
	public class InvertGridGen : GridGen
	{
		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			for (int x = area.xMin; x < area.xMax; x++)
				for (int y = area.yMin; y < area.yMax; y++)
					if (grid.Get(x, y) == 0)
						grid.Set(x, y, 1);
					else
						grid.Set(x, y, 0);

			return grid;
		}
	}
}
