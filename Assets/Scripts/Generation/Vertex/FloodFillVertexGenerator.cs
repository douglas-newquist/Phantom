using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Flood Fill Small")]
	public class FloodFillVertexGenerator : VertexGenerator
	{
		[MinMax(0f, 0.1f)]
		public FloatRange percentageSize = 0.01f;

		public bool areasSmallerThanPercentage = true;

		public int value = 1;

		public override VertexTileMap ApplyOnce(VertexTileMap grid, RectInt area)
		{
			grid = new VertexTileMap(grid);

			var minSize = area.width * area.height * percentageSize.Random;

			foreach (var group in grid.Vertices.FloodFindGroups(area, (v1, v2) => v1 == v2))
			{
				if (group.Count < minSize && grid.Vertices.Get(group[0].x, group[0].y) != value)
					foreach (var cell in group)
						grid.Vertices.TrySet(cell.x, cell.y, value);
			}

			return grid;
		}
	}
}
