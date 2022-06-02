using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Generation/Tiles/Perlin Noise")]
	public class PerlinNoiseGridGen : GridGen
	{
		[MinMax(0, 1)]
		public FloatRange thresholdHeight = new FloatRange(0f, 1f);

		public FloatRange scale = new FloatRange(0.5f, 0.5f);

		public int high = 1, low = 0;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);

			float xScale = scale.Random, yScale = scale.Random;
			float xStep = xScale / area.width;
			float yStep = yScale / area.height;
			float yStart = Random.Range(0f, 1f);
			float X = Random.Range(0f, 1f);
			float threshold = thresholdHeight.Random;

			for (int x = area.xMin; x < area.xMax; x++, X += xStep)
			{
				float Y = yStart;
				for (int y = area.yMin; y < area.yMax; y++, Y += yStep)
				{
					var height = Mathf.PerlinNoise(X, Y);
					grid.Set(x, y, height >= threshold ? high : low);
				}
			}

			return grid;
		}
	}
}
