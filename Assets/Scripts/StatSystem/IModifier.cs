using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public interface IModifier
	{
		object Source { get; }
		int Order { get; }
		bool Stacks { get; }
		float Stack(float magnitude);
		float Apply(float value, float magnitude);
	}

	public static class IModifierHelper
	{
		public static float ApplyModifiers(this IEnumerable<IModifier> modifiers, float value)
		{
			var groups = modifiers.OrderByDescending(mod => mod.Stacks).OrderBy(mod => mod.GetType().ToString()).GroupBy(mod => mod.Order);

			foreach (var group in groups)
			{
				float magnitude = 0;
				IModifier mod = null;

				foreach (var modifier in group)
				{
					if (mod == null) mod = modifier;
					magnitude = modifier.Stack(magnitude);
				}

				value = mod.Apply(value, magnitude);
			}

			return value;
		}
	}
}