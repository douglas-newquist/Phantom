using System.Collections.Generic;

namespace Game
{
	/// <summary>
	/// Describes a collection of rooms that are related somehow
	/// </summary>
	[System.Serializable]
	public class DungeonRegionSpec
	{
		public int id;

		public int superRegion = 0;

		/// <summary>
		/// What rooms are contained in this region
		/// </summary>
		public List<DungeonRoomSpec> rooms = new List<DungeonRoomSpec>();

		/// <summary>
		/// Corridors between rooms inside this region
		/// </summary>
		public List<DungeonCorridorSpec> corridors = new List<DungeonCorridorSpec>();
	}
}
