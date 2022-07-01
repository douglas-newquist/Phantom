using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = ModifierType.CreateMenu + "Set")]
	public class AssignModifierType : ModifierType
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new AssignModifier(source, order, stacks, magnitude);
		}
	}
}
