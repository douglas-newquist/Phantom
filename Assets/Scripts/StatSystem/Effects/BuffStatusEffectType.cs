using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.StatusEffect + "Status Effect")]
	public class BuffStatusEffectType : StatusEffectType
	{
		public Modifier[] modifiers;

		public override IStatusEffect Apply(StatSheet statSheet, object source)
		{
			var buff = new BuffStatusEffect(this, source);
			buff.Apply(statSheet);
			return buff;
		}
	}
}
