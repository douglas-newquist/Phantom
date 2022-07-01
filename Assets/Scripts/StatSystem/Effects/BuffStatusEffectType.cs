using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = StatusEffectType.CreateMenu + "Status Effect")]
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
