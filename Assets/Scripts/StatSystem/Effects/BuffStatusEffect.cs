using System.Collections;
using UnityEngine;

namespace Phantom.StatSystem
{
	public class BuffStatusEffect : StatusEffect
	{
		float duration;

		public BuffStatusEffect(StatusEffectType type, object source) : base(type, source)
		{
		}

		protected override bool PreEffect(StatSheet statSheet)
		{
			BuffStatusEffectType buff = (BuffStatusEffectType)Type;

			foreach (var modifier in buff.modifiers)
				modifier.Apply(statSheet, Source);

			duration = buff.Duration.Random;

			return base.PreEffect(statSheet);
		}

		protected override IEnumerator DoEffect(StatSheet statSheet)
		{
			if (duration >= 0)
			{
				yield return new WaitForSeconds(duration);
				PostEffect(statSheet);
			}
		}

		protected override void PostEffect(StatSheet statSheet)
		{
			statSheet.RemoveAllModifiersFromSource(Source);
			base.PostEffect(statSheet);
		}
	}
}
