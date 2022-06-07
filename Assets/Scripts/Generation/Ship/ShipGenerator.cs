using UnityEngine;

namespace Phantom
{
	public abstract class ShipGenerator : ScriptableObject
	{
		[MinMax(0, 16)]
		public IntRange repeat = new IntRange(1, 1);

		public abstract ShipDesign ApplyOnce(ShipDesign design, RectInt area);

		public virtual ShipDesign Apply(ShipDesign design, RectInt area)
		{
			for (int repeats = repeat.Random; repeats > 0; repeats--)
				design = ApplyOnce(design, area);

			return design;
		}

		public virtual ShipDesign Apply(ShipDesign design)
		{
			var area = new RectInt(0, 0, design.Width, design.Height);
			return Apply(design, area);
		}

		public virtual ShipDesign Create(int width, int height)
		{
			return Apply(new ShipDesign(width, height));
		}
	}
}
