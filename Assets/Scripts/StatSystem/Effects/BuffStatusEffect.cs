using System.Collections;
using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.StatusEffect + "Status Effect")]
	public class BuffStatusEffect : StatusEffect
	{
		protected class Buff : RuntimeStatusEffect
		{
			public Buff(StatusEffect type, object source) : base(type, source)
			{
			}

			protected override IEnumerator DoEffect(StatSheet statSheet)
			{
				var buff = (BuffStatusEffect)Type;

				foreach (var modifier in buff.modifiers)
					modifier.Apply(statSheet, Source);

				yield return new WaitForSeconds(buff.Duration.Random);

				statSheet.RemoveAllModifiersFromSource(Source);
				statSheet.OnStatusEffectExpired.Invoke(this);
			}
		}

		public Modifier[] modifiers;

		public override IStatusEffect Apply(StatSheet statSheet, object source)
		{
			var buff = new Buff(this, source);
			buff.Apply(statSheet);
			return buff;
		}
	}
}
