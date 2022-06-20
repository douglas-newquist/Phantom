using System.Collections;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.StatusEffect + "Damage Over Time")]
	public class DamageOverTimeStatusEffect : StatusEffect
	{
		protected class DoT : RuntimeStatusEffect
		{
			public DoT(StatusEffect type, object source) : base(type, source)
			{
			}

			protected override IEnumerator DoEffect(StatSheet statSheet)
			{
				var dot = (DamageOverTimeStatusEffect)Type;

				int ticks = dot.DamageTicks.Random;
				float duration = dot.Duration.Random;
				float delay = duration / ticks;

				for (int tick = 0; tick < ticks; tick++)
				{
					yield return new WaitForSeconds(delay);
					statSheet.ApplyDamage(dot.damage);
				}
			}
		}

		[MinMax(1, 32)]
		private IntRange damageTicks = new IntRange(3, 5);

		public IntRange DamageTicks => damageTicks;

		public Damage damage;

		public override IStatusEffect Apply(StatSheet statSheet, object source)
		{
			var dot = new DoT(this, source);
			dot.Apply(statSheet);
			return dot;
		}
	}
}
