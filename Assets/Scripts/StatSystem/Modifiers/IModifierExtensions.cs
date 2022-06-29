using System.Collections.Generic;
using System.Linq;

namespace Phantom.StatSystem
{
	public static class IModifierExtensions
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
