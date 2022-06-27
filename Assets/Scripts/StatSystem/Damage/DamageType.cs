using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Damage Type")]
	public class DamageType : ScriptableObject
	{
		public DamageMultiplier[] multipliers;

		public override string ToString()
		{
			return name;
		}
	}
}
