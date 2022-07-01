using System.Collections;
using UnityEngine;

namespace Phantom.StatSystem
{
	public class DamageOverTimeStatusEffect : StatusEffect
	{
		DamageOverTimeStatusEffectType dot;
		IDamageable damageable;

		int ticks;
		float delay;

		public DamageOverTimeStatusEffect(StatusEffectType type, object source) : base(type, source)
		{
		}

		protected override bool PreEffect(StatSheet statSheet)
		{
			dot = (DamageOverTimeStatusEffectType)Type;
			damageable = statSheet.GetComponent<IDamageable>();
			if (damageable == null) return false;

			ticks = dot.DamageTicks.Random;
			if (ticks <= 0) return false;

			float duration = dot.Duration.Random;
			delay = duration / ticks;

			return base.PreEffect(statSheet);
		}

		protected override IEnumerator DoEffect(StatSheet statSheet)
		{
			for (int tick = 0; tick < ticks; tick++)
			{
				yield return new WaitForSeconds(delay);
				damageable.ApplyDamage(dot.damage);
			}
		}
	}
}
