using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Set")]
	public class AssignModifierSO : ModifierSO
	{
		public override IModifier Create(object source, float magnitude)
		{
			return new AssignModifier(source, order, stacks, magnitude);
		}
	}
}
