using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.StatusEffect + "Damage Over Time")]
	public class DamageOverTimeStatusEffectType : StatusEffectType
	{

		[MinMax(1, 32)]
		[SerializeField]
		private IntRange damageTicks = new IntRange(3, 5);

		public IntRange DamageTicks => damageTicks;

		public Damage damage;

		public override IStatusEffect Apply(StatSheet statSheet, object source)
		{
			var dot = new DamageOverTimeStatusEffect(this, source);
			dot.Apply(statSheet);
			return dot;
		}
	}
}
