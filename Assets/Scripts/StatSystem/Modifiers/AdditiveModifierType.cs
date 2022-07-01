using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = ModifierType.CreateMenu + "Add")]
	public class AdditiveModifierType : ModifierType
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new AdditiveModifier(source, order, stacks, magnitude);
		}
	}
}
