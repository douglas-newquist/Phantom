using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = ModifierType.CreateMenu + "Multiply")]
	public class MultiplierModifierType : ModifierType
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new MultiplierModifier(source, order, stacks, magnitude);
		}
	}
}
