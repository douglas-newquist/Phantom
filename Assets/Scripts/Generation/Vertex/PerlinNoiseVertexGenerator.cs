using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Perlin Noise")]
	public sealed class PerlinNoiseVertexGenerator : VertexGenerator
	{
		[MinMax(0, 1)]
		public FloatRange thresholdHeight = new FloatRange(0f, 1f);

		public FloatRange scale = new FloatRange(0.5f, 0.5f);

		public int high = 1, low = 0;

		protected override VertexTileMap ApplyOnce(VertexTileMap design, RectInt area)
		{
			design = new VertexTileMap(design);

			float xScale = scale.Random, yScale = scale.Random;
			float xStep = xScale / area.width;
			float yStep = yScale / area.height;
			float yStart = Random.Range(0f, 1f);
			float X = Random.Range(0f, 1f);
			float threshold = thresholdHeight.Random;

			for (int x = area.xMin; x <= area.xMax; x++, X += xStep)
			{
				float Y = yStart;
				for (int y = area.yMin; y <= area.yMax; y++, Y += yStep)
				{
					var height = Mathf.PerlinNoise(X, Y);
					design.Vertices.TrySet(x, y, height >= threshold ? high : low);
				}
			}

			return design;
		}
	}
}
