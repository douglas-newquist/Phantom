using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Percentage")]
	public class PercentageModifierType : ModifierType
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new PercentageModifier(source, order, stacks, magnitude);
		}
	}
}
