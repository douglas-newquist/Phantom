namespace Game
{
	/// <summary>
	/// Defines a connection between two rooms
	/// </summary>
	[System.Serializable]
	public class DungeonCorridorSpec
	{
		/// <summary>
		/// Starting room id
		/// </summary>
		public int source;

		/// <summary>
		/// Ending room id
		/// </summary>
		public int destination;

		/// <summary>
		/// How wide the corridor is
		/// </summary>
		public DungeonSize size;
	}
}
