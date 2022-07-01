using UnityEngine;

namespace Phantom.StatSystem
{
	public abstract class StatusEffectType : ScriptableObject
	{
		public const string CreateMenu = "Game/Stat System/Status Effect/";

		public const float MaxDuration = 60 * 60;

		[SerializeField]
		private bool effectIsPositive = true;

		public bool IsPositive => effectIsPositive;

		public bool IsNegative => !effectIsPositive;

		[SerializeField]
		private bool canCancel = true;

		/// <summary>
		/// Can this effect be manually canceled
		/// </summary>
		public bool CanCancel => canCancel;

		[SerializeField]
		private bool allowMultiple = false;

		/// <summary>
		/// Can this effect be applied multiple concurrent times
		/// </summary>
		public bool AllowMultiple => allowMultiple;

		[SerializeField]
		[MinMax(-1, MaxDuration)]
		private FloatRange duration = new FloatRange(30, 30);

		public FloatRange Duration => duration;

		public virtual IStatusEffect Apply(StatSheet statSheet)
		{
			return Apply(statSheet, this);
		}

		public abstract IStatusEffect Apply(StatSheet statSheet, object source);
	}
}
