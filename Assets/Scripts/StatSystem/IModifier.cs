using System.Collections.Generic;
using System.Linq;

namespace Phantom
{
	public interface IModifier
	{
		/// <summary>
		/// What is giving this modifier
		/// </summary>
		object Source { get; }

		/// <summary>
		/// What order to apply modifiers in, lowest first
		/// </summary>
		int Order { get; }

		/// <summary>
		/// Does this modifier stack with similar ones
		/// </summary>
		bool Stacks { get; }

		/// <summary>
		/// Magnitude of this modifier
		/// </summary>
		float Magnitude { get; }

		/// <summary>
		/// Stacks the magnitude of two modifiers
		/// </summary>
		/// <param name="magnitude">Current magnitude</param>
		/// <returns>New magnitude</returns>
		float Stack(float magnitude);

		/// <summary>
		/// Applies this modifier to the given stat
		/// </summary>
		/// <param name="statSheet">Stat sheet to apply to</param>
		/// <param name="value">Current value of the stat</param>
		/// <param name="magnitude">Magnitude of this modifier</param>
		/// <returns>New stat value</returns>
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
