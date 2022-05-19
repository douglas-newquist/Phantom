using UnityEngine;

namespace Game
{
	public abstract class Modifier : IModifier
	{
		public virtual object Source { get; protected set; }

		public virtual int Order { get; protected set; }

		public virtual bool Stacks { get; protected set; }

		public virtual float Magnitude { get; protected set; }

		public abstract float Apply(float value, float magnitude);

		public virtual float Stack(float magnitude)
		{
			if (Stacks)
				return Apply(magnitude, Magnitude);

			return Mathf.Max(magnitude, Magnitude);
		}
	}
}