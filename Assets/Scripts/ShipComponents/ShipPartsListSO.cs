using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "List")]
	public class ShipPartsListSO : ScriptableObject
	{
		public ShipPartSO[] parts;
	}
}
