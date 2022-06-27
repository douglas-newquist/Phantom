namespace Phantom.StatSystem
{
	public sealed class LinkModifier : IModifier
	{
		public LinkModifier(object source, int order, bool stacks, float magnitude, StatType linkedType, IModifier linkedModifier)
		{
			Source = source;
			Order = order;
			Stacks = stacks;
			Magnitude = magnitude;
			LinkedType = linkedType;
			LinkedModifier = linkedModifier;
		}

		public object Source { get; private set; }

		public int Order { get; private set; }

		public bool Stacks { get; private set; }

		public float Magnitude { get; private set; }

		public StatType LinkedType { get; private set; }

		public IModifier LinkedModifier { get; private set; }

		public float Apply(StatSheet statSheet, float value, float magnitude)
		{
			return value + LinkedModifier.Apply(statSheet, statSheet.GetValue(LinkedType), magnitude);
		}

		public float Stack(float magnitude)
		{
			// TODO handle different types
			return Magnitude + magnitude;
		}
	}
}
