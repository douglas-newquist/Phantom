using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.Stats + "Basic")]
	public class StatType : ScriptableObject, ITooltip
	{
		[SerializeField]
		private string displayName;

		public string DisplayName => displayName;

		[SerializeField]
		[TextArea]
		private string description = "No description provided.";

		public string Description => description;

		[SerializeField]
		private Sprite icon;

		public Sprite Icon => icon;

		[SerializeField]
		private float defaultValue;

		public float DefaultValue => defaultValue;

		[SerializeField]
		private FloatRange limits = new FloatRange(0, float.MaxValue);

		public FloatRange Limits => limits;

		[SerializeField]
		private bool canBeModified = true;

		public bool CanBeModified => canBeModified;

		public virtual IStat Create()
		{
			return new Stat(this, DefaultValue);
		}
	}
}
