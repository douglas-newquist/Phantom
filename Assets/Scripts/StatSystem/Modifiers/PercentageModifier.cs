using UnityEngine;

namespace Phantom.StatSystem
{
	/// <summary>
	/// Increases a stat value by a percentage (1 + magnitude)%, stacks additively
	/// </summary>
	public sealed class PercentageModifier : IModifier
	{
		public PercentageModifier(object source, int order, bool stacks, float magnitude)
		{
			Source = source;
			Order = order;
			Stacks = stacks;
			Magnitude = magnitude;
		}

		public object Source { get; private set; }

		public int Order { get; private set; }

		public bool Stacks { get; private set; }

		public float Magnitude { get; private set; }

		public float Apply(StatSheet statSheet, float value, float magnitude)
		{
			return (magnitude + 1) * value;
		}

		public float Stack(float magnitude)
		{
			if (Stacks)
				return magnitude + Magnitude;

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}
