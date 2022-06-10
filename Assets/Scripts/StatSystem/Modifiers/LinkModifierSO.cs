using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Modifiers + "Link")]
	public class LinkModifierSO : ModifierSO
	{
		public StatSO stat;

		public override IModifier Create(object source, float magnitude)
		{
			throw new System.NotImplementedException();
		}
	}
}
