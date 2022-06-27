using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Condition + "Combat Status")]
	public class CombatStatusCondition : Condition
	{
		public bool inCombat = true;

		public override bool Satisfied(GameObject gameObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
