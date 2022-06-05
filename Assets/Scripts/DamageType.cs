using UnityEngine;

namespace Game
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
