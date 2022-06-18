using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Flood Fill Small")]
	public class FloodFillGridGen : GridGen
	{
		[MinMax(0f, 0.5f)]
		public FloatRange percentageSize = 0.05f;

		public bool areasSmallerThanPercentage = true;

		public int value = 1;

		public override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			var minSize = area.width * area.height * percentageSize.Random;

			foreach (var group in grid.FloodFindGroups(area, (v1, v2) => v1 == v2))
			{
				if (group.Count < minSize && grid.Get(group[0].x, group[0].y) != value)
					foreach (var cell in group)
						grid.Set(cell.x, cell.y, value);
			}

			return grid;
		}
	}
}
