using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Mirror")]
	public sealed class MirrorVertexGenerator : VertexGenerator
	{
		public Mirror mirror;

		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			design = new VertexTileMap(design);

			switch (mirror)
			{
				case Mirror.X:
					MirrorX(design, area);
					break;

				case Mirror.Y:
					MirrorY(design, area);
					break;

				case Mirror.XY:
					MirrorX(design, area);
					MirrorY(design, area);
					break;

				case Mirror.YX:
					MirrorY(design, area);
					MirrorX(design, area);
					break;
			}

			return design;
		}

		public void MirrorX(VertexTileMap design, RectInt area)
		{
			for (int x = area.xMin; x <= area.xMax; x++)
			{
				int y1 = area.yMin;
				int y2 = area.yMax;

				for (; y1 < y2; y1++, y2--)
					if (design.Vertices.TryGet(x, y2, out var vertex))
						design.Vertices.Set(x, y2, vertex);
			}
		}

		public void MirrorY(VertexTileMap design, RectInt area)
		{
			for (int y = area.yMin; y <= area.yMax; y++)
			{
				int x1 = area.xMin;
				int x2 = area.xMax;

				for (; x1 < x2; x1++, x2--)
					if (design.Vertices.TryGet(x1, y, out var vertex))
						design.Vertices.Set(x2, y, vertex);
			}
		}
	}
}
