using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Hull Selector")]
	public class HullSelectorShipGenerator : ScriptableObject
	{
		public WeightedList<TileMapSO> hulls;

		public TileMapSO GetHull()
		{
			return hulls.GetRandom();
		}
	}
}
