using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Parts/List")]
	public class ShipPartsListSO : ScriptableObject
	{
		public ShipPartSO[] parts;
	}
}
