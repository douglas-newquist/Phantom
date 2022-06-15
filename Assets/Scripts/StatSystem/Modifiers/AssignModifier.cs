using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Sets a stat value to a constant value
	/// </summary>
	public sealed class AssignModifier : IModifier
	{
		public AssignModifier(object source, int order, bool stacks, float magnitude)
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
			return magnitude;
		}

		public float Stack(float magnitude)
		{
			if (Stacks)
				return magnitude + Magnitude;

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}
