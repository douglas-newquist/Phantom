using UnityEngine;

namespace Phantom
{
	public abstract class WorldSensor : ScriptableObject
	{
		[SerializeField]
		private string key;

		public string Key => key;

		public const string CreateMenu = "Game/AI/Sensors/";

		public abstract WorldState GetWorldState(GameObject gameObject);
	}
}
