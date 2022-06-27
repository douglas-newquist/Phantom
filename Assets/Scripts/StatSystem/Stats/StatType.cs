using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Stats + "Basic")]
	public class StatType : ScriptableObject, ITooltip
	{
		public string displayName;

		public string DisplayName => displayName;

		[TextArea]
		public string description = "No description provided.";

		public string Description => description;

		public Sprite icon;

		public Sprite Icon => icon;

		public float defaultValue;

		public FloatRange limits = new FloatRange(0, float.MaxValue);

		public bool canBeModified = true;

		public virtual IStat Create()
		{
			return new Stat(this, defaultValue);
		}
	}
}
