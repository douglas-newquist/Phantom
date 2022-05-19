using UnityEngine;

namespace Game
{
	public class AdditiveModifier : Modifier
	{
		public AdditiveModifier(object source, int order, bool stacks, float magnitude)
		{
			Source = source;
			Order = order;
			Stacks = stacks;
			Magnitude = magnitude;
		}

		public override float Apply(float value, float magnitude)
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
