using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Percentage")]
	public class PercentageModifierSO : ModifierSO
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new PercentageModifier(source, order, stacks, magnitude);
		}
	}
}
