using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Multiply")]
	public class MultiplierModifierType : ModifierType
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new MultiplierModifier(source, order, stacks, magnitude);
		}
	}
}
