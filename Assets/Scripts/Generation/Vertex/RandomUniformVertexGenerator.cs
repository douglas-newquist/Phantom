using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Random Uniform")]
	public sealed class RandomUniformVertexGenerator : VertexGenerator
	{
		[MinMax(0, 1)]
		public FloatRange chance = new FloatRange(0.4f, 0.6f);

		public int value = 1;

		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			design = new VertexTileMap(design);
			var threshold = chance.Random;

			for (int x = area.xMin; x <= area.xMax; x++)
				for (int y = area.yMin; y < area.yMax; y++)
					if (Random.Range(0f, 1f) <= threshold)
						design.Vertices.TrySet(x, y, value);

			return design;
		}
	}
}
