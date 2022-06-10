using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Add")]
	public class AdditiveModifierSO : ModifierSO
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new AdditiveModifier(source, order, stacks, magnitude);
		}
	}
}
