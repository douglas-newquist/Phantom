using UnityEngine;

namespace Phantom
{
	public abstract class Modifier : IModifier
	{
		public virtual object Source { get; protected set; }

		public virtual int Order { get; protected set; }

		public virtual bool Stacks { get; protected set; }

		public virtual float Magnitude { get; protected set; }

		protected Modifier(object source, int order, bool stacks, float magnitude)
		{
			Source = source;
			Order = order;
			Stacks = stacks;
			Magnitude = magnitude;
		}

		public abstract float Apply(StatSheet statSheet, float value, float magnitude);

		public abstract float Stack(float magnitude);
	}
}
