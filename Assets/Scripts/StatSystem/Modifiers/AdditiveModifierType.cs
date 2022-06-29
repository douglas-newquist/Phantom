using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Add")]
	public class AdditiveModifierType : ModifierType
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new AdditiveModifier(source, order, stacks, magnitude);
		}
	}
}
