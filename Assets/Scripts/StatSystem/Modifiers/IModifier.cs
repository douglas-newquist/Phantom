namespace Phantom.StatSystem
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
}
