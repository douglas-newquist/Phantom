using System.Collections.Generic;

namespace Phantom
{
	/// <summary>
	/// Describes the general structure of a dungeon
	/// </summary>
	[System.Serializable]
	public class DungeonSpec
	{
		public List<DungeonRegionSpec> regions = new List<DungeonRegionSpec>();

		/// <summary>
		/// Corridors between regions inside this region
		/// </summary>
		public List<DungeonCorridorSpec> corridors = new List<DungeonCorridorSpec>();
	}
}
