using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu + "Slice Region Selector")]
	public sealed class SliceRegionSelector : RegionSelector
	{
		[Range(0f, 0.5f)]
		[SerializeField]
		private float minWidth = 0.1f;

		[Range(0f, 0.5f)]
		[SerializeField]
		private float minHeight = 0.1f;

		public override IEnumerable<RectInt> GetRegions(RectInt totalArea)
		{
			int width = (int)(totalArea.width * minWidth);
			int height = (int)(totalArea.height * minHeight);

			foreach (var column in SliceWidth(totalArea, width))
				foreach (var cell in SliceHeight(column, height))
					yield return cell;
		}

		private bool CanSliceHeight(RectInt area, int min)
		{
			return area.height > min * 2;
		}

		private bool CanSliceWidth(RectInt area, int min)
		{
			return area.width > min * 2;
		}

		private IEnumerable<RectInt> SliceWidth(RectInt area, int minWidth)
		{
			if (CanSliceWidth(area, minWidth))
			{
				var r1 = area;
				r1.xMax = Random.Range(area.xMin + minWidth, area.xMax - minWidth);

				foreach (var slice in SliceWidth(r1, minWidth))
					yield return slice;

				var r2 = area;
				r2.xMin = r1.xMax + 1;

				foreach (var slice in SliceWidth(r2, minWidth))
					yield return slice;
			}
			else
				yield return area;
		}

		private IEnumerable<RectInt> SliceHeight(RectInt area, int minHeight)
		{
			if (CanSliceHeight(area, minHeight))
			{
				var r1 = area;
				r1.yMax = Random.Range(area.yMin + minHeight, area.yMax - minHeight);

				foreach (var slice in SliceHeight(r1, minHeight))
					yield return slice;

				var r2 = area;
				r2.yMin = r1.yMax + 1;

				foreach (var slice in SliceHeight(r2, minHeight))
					yield return slice;
			}
			else
				yield return area;
		}
	}
}
