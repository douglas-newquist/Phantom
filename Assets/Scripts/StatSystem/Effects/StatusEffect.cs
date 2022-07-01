using System.Collections;
using UnityEngine;

namespace Phantom.StatSystem
{
	public abstract class StatusEffect : IStatusEffect
	{
		public StatusEffectType Type { get; set; }

		public object Source { get; set; }

		public Coroutine Coroutine { get; set; }

		public bool IsRunning => Coroutine != null;

		protected StatusEffect(StatusEffectType type, object source)
		{
			Type = type;
			Source = source;
		}

		protected abstract IEnumerator DoEffect(StatSheet statSheet);

		public virtual void Apply(StatSheet statSheet)
		{
			if (!IsRunning)
			{
				statSheet.OnStatusEffectApplied.Invoke(this);
				Coroutine = statSheet.StartCoroutine(DoEffect(statSheet));
			}
		}

		public void Cancel(StatSheet statSheet)
		{
			if (IsRunning)
			{
				statSheet.StopCoroutine(Coroutine);
				statSheet.OnStatusEffectExpired.Invoke(this);
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
}
