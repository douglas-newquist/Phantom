using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Flood Fill Small")]
	public sealed class FloodFillVertexGenerator : VertexGenerator
	{
		[MinMax(0f, 0.1f)]
		public FloatRange percentageSize = 0.01f;

		public bool areasSmallerThanPercentage = true;

		public int value = 1;

		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			design = new VertexTileMap(design);

			var minSize = area.width * area.height * percentageSize.Random;

			foreach (var group in design.Vertices.FloodFindGroups(area, (v1, v2) => v1 == v2))
			{
				if (group.Count < minSize && design.Vertices.Get(group[0].x, group[0].y) != value)
					foreach (var cell in group)
						design.Vertices.TrySet(cell.x, cell.y, value);
			}

			return design;
		}
	}
}
