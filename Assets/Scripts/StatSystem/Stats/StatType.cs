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

		/// <summary>
		/// The default value of this stat
		/// </summary>
		public float DefaultValue => defaultValue;

		[SerializeField]
		private FloatRange limits = new FloatRange(0, float.MaxValue);

		/// <summary>
		/// Range of valid values for this stat
		/// </summary>
		public FloatRange Limits => limits;

		[SerializeField]
		private bool canBeModified = true;

		/// <summary>
		/// Can modifiers be applied to this stat
		/// </summary>
		public bool CanBeModified => canBeModified;

		[SerializeField]
		private StatusEffectType[] statusEffects;

		/// <summary>
		/// Status effects to apply to a stat sheet that has this stat
		/// </summary>
		public StatusEffectType[] StatusEffects => statusEffects;

		public virtual IStat Create()
		{
			return new Stat(this, DefaultValue);
		}

		/// <summary>
		/// Called when a stat of this type is added to a stat sheet
		/// </summary>
		/// <param name="statSheet">Stat sheet the stat was added to</param>
		/// <param name="stat">The stat added</param>
		public virtual void OnAddToStatSheet(StatSheet statSheet, IStat stat)
		{
			foreach (var statusEffect in StatusEffects)
				statusEffect.Apply(statSheet);
		}
	}
}
