namespace Phantom
{
	public enum Reservation
	{
		/// <summary>
		/// Open to be used
		/// </summary>
		Free,

		/// <summary>
		/// Currently in use
		/// </summary>
		Used,

		/// <summary>
		/// Reserved by someone else
		/// </summary>
		Reserved,

		/// <summary>
		/// Cannot be used
		/// </summary>
		Locked
	}
}
