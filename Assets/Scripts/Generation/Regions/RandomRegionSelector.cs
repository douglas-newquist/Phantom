using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu + "Random Region")]
	public sealed class RandomRegionSelector : RegionSelector
	{
		[MinMax(0, 32)]
		public IntRange regionCount = new IntRange(1, 1);

		[MinMax(0, 1)]
		public FloatRange width = 0.5f, height = 0.5f;

		public override IEnumerable<RectInt> GetRegions(RectInt totalArea)
		{
			for (int region = regionCount.Random; region > 0; region--)
			{
				int w = (int)(totalArea.width * width.Random);
				int h = (int)(totalArea.height * height.Random);
				int x = Random.Range(totalArea.xMin, totalArea.xMax - w);
				int y = Random.Range(totalArea.yMin, totalArea.yMax - h);

				yield return new RectInt(x, y, w, h);
			}
		}
	}
}
