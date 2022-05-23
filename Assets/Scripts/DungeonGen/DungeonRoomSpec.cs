using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// Outlines rough details about a room's size and components
	/// </summary>
	[System.Serializable]
	public class DungeonRoomSpec
	{
		public int id;

		public bool isEntryPoint = false;

		/// <summary>
		/// How large the room is
		/// </summary>
		public DungeonSize size;

		/// <summary>
		/// The number of outward connections to other rooms
		/// </summary>
		[Range(0, 16)]
		public int connections;

		/// <summary>
		/// What important objects are inside this room
		/// </summary>
		public List<DungeonElementSpec> elements = new List<DungeonElementSpec>();
	}
}
