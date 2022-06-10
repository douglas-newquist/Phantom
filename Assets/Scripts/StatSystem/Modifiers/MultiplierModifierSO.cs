using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Multiply")]
	public class MultiplierModifierSO : ModifierSO
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new MultiplierModifier(source, order, stacks, magnitude);
		}
	}
}
