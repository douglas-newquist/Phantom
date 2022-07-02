using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Invert")]
	public sealed class InvertVertexGenerator : VertexGenerator
	{
		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			design = new VertexTileMap(design);

			for (int x = area.xMin; x <= area.xMax; x++)
				for (int y = area.yMin; y <= area.yMax; y++)
					if (design.Vertices.Get(x, y) == 0)
						design.Vertices.Set(x, y, 1);
					else
						design.Vertices.Set(x, y, 0);

			return design;
		}
	}
}
