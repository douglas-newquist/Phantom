using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Adds a constant value to a stat value
	/// </summary>
	public class AdditiveModifier : Modifier
	{
		public AdditiveModifier(object source, int order, bool stacks, float magnitude) : base(source, order, stacks, magnitude)
		{
		}

		public override float Apply(StatSheet statSheet, float value, float magnitude)
		{
			return value + magnitude;
		}

		public override float Stack(float magnitude)
		{
			if (Stacks)
				return magnitude + Magnitude;

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}
