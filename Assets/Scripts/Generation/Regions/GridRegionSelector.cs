using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Breaks the given total area into a grid of regions
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu + "Grid of Regions")]
	public sealed class GridRegionSelector : RegionSelector
	{
		[MinMax(1, 64)]
		public IntRange x = new IntRange(2, 4), y = new IntRange(2, 4);

		[MinMax(-1, 64)]
		public IntRange xSpacing = new IntRange(1, 1);

		[MinMax(-1, 64)]
		public IntRange ySpacing = new IntRange(1, 1);

		public Slice slice;

		public enum Slice
		{
			CellCount,
			CellSize
		}

		public override IEnumerable<RectInt> GetRegions(RectInt totalArea)
		{
			int width = x.Random;
			int height = y.Random;
			int xSpace = xSpacing.Random;
			int ySpace = ySpacing.Random;

			if (slice == Slice.CellCount)
				return GetCountRegions(totalArea, width, xSpace, height, ySpace);

			return GetRegionsOfSize(totalArea, x.Random, xSpace, y.Random, ySpace);
		}

		public IEnumerable<RectInt> GetCountRegions(RectInt area, int xCells, int xSpace, int yCells, int ySpace)
		{
			int width = area.width - (xCells - 1) * xSpace;
			width /= xCells;

			int height = area.height - (yCells - 1) * ySpace;
			height /= yCells;

			return GetRegionsOfSize(area, width, xSpace, height, ySpace);
		}

		/// <summary>
		/// Breaks the given area into subregions
		/// </summary>
		/// <param name="area">Total area to slice</param>
		/// <param name="width">Desired width of each sub region</param>
		/// <param name="height">Desired height of each sub region</param>
		/// <returns></returns>
		public IEnumerable<RectInt> GetRegionsOfSize(RectInt area, int width, int xSpace, int height, int ySpace)
		{
			for (int x = area.xMin; x < area.xMax; x += width + xSpace)
			{
				for (int y = area.yMin; y < area.yMax; y += height + ySpace)
				{
					var region = new RectInt(x, y, width, height);
					region.xMax = Mathf.Clamp(region.xMax, area.xMin, area.xMax);
					region.yMax = Mathf.Clamp(region.yMax, area.yMin, area.yMax);
					yield return region;
				}
			}
		}
	}
}
