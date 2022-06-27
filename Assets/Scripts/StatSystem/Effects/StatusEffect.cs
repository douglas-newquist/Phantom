using System.Collections;
using UnityEngine;

namespace Phantom.StatSystem
{
	public abstract class StatusEffect : ScriptableObject
	{
		protected abstract class RuntimeStatusEffect : IStatusEffect
		{
			public StatusEffect Type { get; set; }

			public object Source { get; set; }

			public Coroutine Coroutine { get; set; }

			public bool IsRunning => Coroutine != null;

			protected RuntimeStatusEffect(StatusEffect type, object source)
			{
				Type = type;
				Source = source;
			}

			protected abstract IEnumerator DoEffect(StatSheet statSheet);

			public virtual void Apply(StatSheet statSheet)
			{
				if (!IsRunning)
					Coroutine = statSheet.StartCoroutine(DoEffect(statSheet));
			}

			public void Cancel(StatSheet statSheet)
			{
				if (IsRunning)
				{
					statSheet.StopCoroutine(Coroutine);
					Coroutine = null;
				}
			}

			public bool TryCancel(StatSheet statSheet)
			{
				if (!IsRunning) return true;
				if (Type.CanCancel)
				{
					Cancel(statSheet);
					return true;
				}

				return false;
			}
		}

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
