using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Marching Squares")]
	public sealed class MarchingSquaresVertexGenerator : VertexGenerator
	{
		[MinMax(0, 8)]
		public IntRange n;

		public int alive = 1, dead = 0;

		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			var result = new VertexTileMap(design);

			for (int x = area.xMin; x <= area.xMax; x++)
				for (int y = area.yMin; y <= area.yMax; y++)
				{
					int active = 0;
					foreach (var neighbor in design.Vertices.GetNeighbors(new Vector2Int(x, y), 1, true))
						if (neighbor.Value == alive)
							active++;

					if (active <= n.Min)
						result.Vertices.TrySet(x, y, dead);
					else if (active >= n.Max)
						result.Vertices.TrySet(x, y, alive);
				}

			return result;
		}
	}
}
