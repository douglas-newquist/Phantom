using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Border")]
	public sealed class BorderVertexGenerator : VertexGenerator
	{
		[MinMax(0, 32)]
		public IntRange topBorder = new IntRange(3, 3);

		[MinMax(0, 32)]
		public IntRange bottomBorder = new IntRange(3, 3);

		[MinMax(0, 32)]
		public IntRange leftBorder = new IntRange(3, 3);

		[MinMax(0, 32)]
		public IntRange rightBorder = new IntRange(3, 3);

		public int value = 1;

		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			design = new VertexTileMap(design);

			TopBorder(design, area);
			BottomBorder(design, area);
			LeftBorder(design, area);
			RightBorder(design, area);

			return design;
		}

		public void BottomBorder(VertexTileMap design, RectInt area)
		{
			for (int x = area.xMin; x <= area.xMax; x++)
			{
				int depth = bottomBorder.Random;

				for (int yi = 0; yi < depth; yi++)
					design.Vertices.TrySet(x, area.yMin + yi, value);
			}
		}

		public void TopBorder(VertexTileMap design, RectInt area)
		{
			for (int x = area.xMin; x <= area.xMax; x++)
			{
				int depth = topBorder.Random;

				for (int yi = 0; yi < depth; yi++)
					design.Vertices.TrySet(x, area.yMax - yi, value);
			}
		}

		public void LeftBorder(VertexTileMap design, RectInt area)
		{
			for (int y = area.yMin; y <= area.yMax; y++)
			{
				int depth = leftBorder.Random;

				for (int xi = 0; xi < depth; xi++)
					design.Vertices.TrySet(area.xMin + xi, y, value);
			}
		}

		public void RightBorder(VertexTileMap design, RectInt area)
		{
			for (int y = area.yMin; y <= area.yMax; y++)
			{
				int depth = rightBorder.Random;

				for (int xi = 0; xi < depth; xi++)
					design.Vertices.TrySet(area.xMax - xi, y, value);
			}
		}
	}
}
