using UnityEngine;

namespace Game
{
	/// <summary>
	/// Sets a stat value to a constant value
	/// </summary>
	public class AssignModifier : Modifier
	{
		public AssignModifier(object source, int order, bool stacks, float magnitude)
		{
			Source = source;
			Order = order;
			Stacks = stacks;
			Magnitude = magnitude;
		}

		public override float Apply(float value, float magnitude)
		{
			return magnitude;
		}

		public override float Stack(float magnitude)
		{
			if (Stacks)
				return magnitude + Magnitude;

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}
