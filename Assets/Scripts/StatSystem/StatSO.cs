using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Stats + "Basic")]
	public class StatSO : ScriptableObject
	{
		public string displayName;

		[TextArea]
		public string description = "No description provided.";

		public Sprite icon;

		public float defaultValue;

		public FloatRange limits = new FloatRange(0, float.MaxValue);

		public virtual Stat Create()
		{
			return new Stat(this, defaultValue);
		}
	}
}
