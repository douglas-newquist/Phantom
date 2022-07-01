using System.Collections;
using UnityEngine;

namespace Phantom.StatSystem
{
	public class DamageOverTimeStatusEffect : StatusEffect
	{
		public DamageOverTimeStatusEffect(StatusEffectType type, object source) : base(type, source)
		{
		}

		protected override IEnumerator DoEffect(StatSheet statSheet)
		{
			var dot = (DamageOverTimeStatusEffectType)Type;
			var damageable = statSheet.GetComponent<IDamageable>();
			if (damageable == null) yield break;

			int ticks = dot.DamageTicks.Random;
			float duration = dot.Duration.Random;
			float delay = duration / ticks;

			for (int tick = 0; tick < ticks; tick++)
			{
				yield return new WaitForSeconds(delay);
				damageable.ApplyDamage(dot.damage);
			}

			statSheet.OnStatusEffectExpired.Invoke(this);
		}
	}
}
