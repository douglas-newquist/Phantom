using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Set")]
	public class AssignModifierType : ModifierType
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new AssignModifier(source, order, stacks, magnitude);
		}
	}
}
