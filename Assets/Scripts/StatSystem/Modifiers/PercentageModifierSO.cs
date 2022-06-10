using UnityEngine;

namespace Phantom
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
