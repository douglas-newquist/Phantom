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

		/// <summary>
		/// Called before the main effect is called
		/// </summary>
		/// <param name="statSheet">Stat sheet being affected</param>
		protected virtual bool PreEffect(StatSheet statSheet)
		{
			statSheet.OnStatusEffectApplied.Invoke(this);
			return true;
		}

		/// <summary>
		/// Executes effect of this status effect
		/// </summary>
		/// <param name="statSheet">Stat sheet being affected</param>
		protected abstract IEnumerator DoEffect(StatSheet statSheet);

		/// <summary>
		/// Call after the main effect ends or is canceled
		/// </summary>
		/// <param name="statSheet"></param>
		protected virtual void PostEffect(StatSheet statSheet)
		{
			statSheet.OnStatusEffectExpired.Invoke(this);
		}

		public virtual void Apply(StatSheet statSheet)
		{
			if (!IsRunning && PreEffect(statSheet))
			{
				Coroutine = statSheet.StartCoroutine(DoEffect(statSheet));
			}
		}

		public virtual void Cancel(StatSheet statSheet)
		{
			if (IsRunning)
			{
				statSheet.StopCoroutine(Coroutine);
				PostEffect(statSheet);
				Coroutine = null;
			}
		}

		public virtual bool TryCancel(StatSheet statSheet)
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
