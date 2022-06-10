using System.Collections.Generic;
using System.Linq;

namespace Phantom
{
	public interface IModifier
	{
		object Source { get; }
		int Order { get; }
		bool Stacks { get; }
		float Stack(float magnitude);
		float Apply(StatSheet statSheet, float value, float magnitude);
	}

	public static class IModifierHelper
	{
		public static float ApplyModifiers(this IEnumerable<IModifier> modifiers, StatSheet statSheet, float value)
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

				value = mod.Apply(statSheet, value, magnitude);
			}

			return value;
		}
	}
}
