using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Multiplies a stat value by a constant, stacks multiplicative
	/// </summary>
	public sealed class MultiplierModifier : IModifier
	{
		public MultiplierModifier(object source, int order, bool stacks, float magnitude)
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
			return value * magnitude;
		}

		public float Stack(float magnitude)
		{
			if (magnitude == 0)
				return Magnitude;

			if (Stacks)
				return magnitude * Magnitude;

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}
