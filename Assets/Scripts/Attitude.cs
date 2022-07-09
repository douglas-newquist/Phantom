namespace Phantom
{
	/// <summary>
	/// Defines the general stance of an entity towards another entity
	/// </summary>
	[System.Flags]
	public enum Attitude
	{
		/// <summary>
		/// Entity has no attitude or is incapable of having one to another entity
		/// </summary>
		NoAttitude,

		/// <summary>
		/// Entity is neither friendly or hostile but will still defend itself
		/// </summary>
		Neutral = 1,

		Friendly = 2,

		Hostile = 4,

		/// <summary>
		/// Entity is unpredictable and will act unpredictably towards other entities
		/// </summary>
		Enigmatic = 8,

		/// <summary>
		/// Entity will completely ignore another
		/// </summary>
		Dismissive = 16,

		/// <summary>
		/// Entities that will not actively harm this one
		/// </summary>
		NonHostile = Neutral | Friendly | Dismissive,

		/// <summary>
		/// Entities that will not actively help this one
		/// </summary>
		NonFriendly = Neutral | Hostile | Enigmatic | Dismissive,

		AnyAttitude = ~0
	}
}
