using System.Collections;
using UnityEngine;

namespace Phantom.StatSystem
{
	public class BuffStatusEffect : StatusEffect
	{
		public BuffStatusEffect(StatusEffectType type, object source) : base(type, source)
		{
		}

		protected override IEnumerator DoEffect(StatSheet statSheet)
		{
			var buff = (BuffStatusEffectType)Type;

			foreach (var modifier in buff.modifiers)
				modifier.Apply(statSheet, Source);

			yield return new WaitForSeconds(buff.Duration.Random);

			statSheet.RemoveAllModifiersFromSource(Source);
			statSheet.OnStatusEffectExpired.Invoke(this);
		}
	}
}
