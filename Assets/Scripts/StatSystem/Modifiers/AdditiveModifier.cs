using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Adds a constant value to a stat value
	/// </summary>
	public sealed class AdditiveModifier : IModifier
	{
		public AdditiveModifier(object source, int order, bool stacks, float magnitude)
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
			return value + magnitude;
		}

		public float Stack(float magnitude)
		{
			if (Stacks)
				return magnitude + Magnitude;

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}
