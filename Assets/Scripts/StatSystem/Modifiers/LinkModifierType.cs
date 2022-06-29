using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Link")]
	public class LinkModifierType : ModifierType
	{
		public StatType stat;

		public ModifierType linkStatRatio;

		public override IModifier Create(object source, float magnitude)
		{
			var modifier = linkStatRatio.Create(magnitude);
			return new LinkModifier(source, order, stacks, magnitude, stat, modifier);
		}
	}
}
