using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Parts/List")]
	public class ShipPartsListSO : ScriptableObject
	{
		public ShipPartSO[] parts;
	}
}
