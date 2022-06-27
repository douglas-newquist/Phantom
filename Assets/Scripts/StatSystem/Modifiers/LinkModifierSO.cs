using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Link")]
	public class LinkModifierSO : ModifierSO
	{
		public StatType stat;

		public ModifierSO linkStatRatio;

		public override IModifier Create(object source, float magnitude)
		{
			var modifier = linkStatRatio.Create(magnitude);
			return new LinkModifier(source, order, stacks, magnitude, stat, modifier);
		}
	}
}
