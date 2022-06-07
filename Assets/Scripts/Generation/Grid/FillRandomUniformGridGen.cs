using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.VertexGenerator + "Random Uniform")]
	public class FillRandomUniformGridGen : GridGen
	{
		[MinMax(0, 1)]
		public FloatRange chance = new FloatRange(0.4f, 0.6f);

		public int value = 1;

		protected override Grid2D<int> ApplyOnce(Grid2D<int> grid, RectInt area)
		{
			grid = new Grid2D<int>(grid);
			var threshold = chance.Random;

			for (int x = area.xMin; x < area.xMax; x++)
				for (int y = area.yMin; y < area.yMax; y++)
					if (Random.Range(0f, 1f) < threshold)
						grid.Set(x, y, value);

			return grid;
		}
	}
}
