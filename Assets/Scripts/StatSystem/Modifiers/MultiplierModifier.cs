using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Multiplies a stat value by a constant, stacks multiplicative
	/// </summary>
	public class MultiplierModifier : Modifier
	{
		public MultiplierModifier(object source, int order, bool stacks, float magnitude) : base(source, order, stacks, magnitude)
		{
		}

		public override float Apply(StatSheet statSheet, float value, float magnitude)
		{
			return value * magnitude;
		}

		public override float Stack(float magnitude)
		{
			if (magnitude == 0)
				return Magnitude;

			if (Stacks)
				return magnitude * Magnitude;

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}
