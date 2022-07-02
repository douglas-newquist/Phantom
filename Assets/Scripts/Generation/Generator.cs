using UnityEngine;

namespace Phantom
{
	public abstract class Generator<T> : ScriptableObject, IGenerator<T>
	{
		[MinMax(0, 16)]
		public IntRange repeat = new IntRange(1, 1);

		public RegionSelector regionSelector;

		public virtual T Apply(T design, RectInt area)
		{
			for (int repeats = repeat.Random; repeats > 0; repeats--)
			{
				if (regionSelector != null)
				{
					foreach (var region in regionSelector.GetRegions(area))
						design = ApplyOnce(design, region);
				}
				else
				{
					design = ApplyOnce(design, area);
				}
			}

			return design;
		}

		public abstract T Apply(T design);

		public abstract T ApplyOnce(T design, RectInt area);

		public abstract T Create(int width, int height);
	}
}
