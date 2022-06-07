using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public abstract class DungeonElement : ScriptableObject
	{

	}
	[System.Serializable]
	public class DungeonElementSpec
	{
		public DungeonElement element;

		/// <summary>
		/// Special value used by the element
		/// </summary>
		public int special;
	}
}
