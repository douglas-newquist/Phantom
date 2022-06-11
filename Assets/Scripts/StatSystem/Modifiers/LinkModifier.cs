namespace Phantom
{
	public class LinkModifier : Modifier
	{
		public StatSO LinkedType { get; protected set; }

		public LinkModifier(object source, int order, bool stacks, float magnitude, StatSO linkedType) : base(source, order, stacks, magnitude)
		{
			LinkedType = linkedType;
		}

		public override float Apply(StatSheet statSheet, float value, float magnitude)
		{
			return value + statSheet.GetValue(LinkedType) * magnitude;
		}

		public override float Stack(float magnitude)
		{
			// TODO handle different types
			return Magnitude + magnitude;
		}
	}
}
