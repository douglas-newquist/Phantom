using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = ModifierType.CreateMenu + "Percentage")]
	public class PercentageModifierType : ModifierType
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new PercentageModifier(source, order, stacks, magnitude);
		}
	}
}
