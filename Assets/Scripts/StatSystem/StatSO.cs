using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Stats/Stat")]
	public class StatSO : ScriptableObject
	{
		public string displayName;

		[TextArea]
		public string description = "No description provided.";

		public Sprite icon;

		public float defaultValue;

		public virtual Stat Create()
		{
			return new Stat(this, defaultValue);
		}
	}
}
