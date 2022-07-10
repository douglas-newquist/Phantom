using UnityEngine;

namespace Phantom
{
	public abstract class WorldSensor : ScriptableObject
	{
		public const string CreateMenu = "Game/AI/Sensors/";

		[SerializeField]
		private string key;

		public string Key => key;

		public abstract WorldState GetWorldState(GameObject gameObject);

		public override int GetHashCode()
		{
			return Key.GetHashCode();
		}
	}
}
