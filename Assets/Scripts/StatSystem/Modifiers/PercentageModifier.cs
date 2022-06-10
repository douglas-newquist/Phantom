using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Increases a stat value by a percentage, stacks additively
	/// </summary>
	public class PercentageModifier : Modifier
	{
		public PercentageModifier(object source, int order, bool stacks, float magnitude) : base(source, order, stacks, magnitude)
		{
		}

		public override float Apply(StatSheet statSheet, float value, float magnitude)
		{
			return (magnitude + 1) * value;
		}

		public override float Stack(float magnitude)
		{
			if (Stacks)
				return magnitude + Magnitude;

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}
