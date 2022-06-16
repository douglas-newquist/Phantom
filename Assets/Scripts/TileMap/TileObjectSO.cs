using UnityEngine;

namespace Phantom
{
	public abstract class TileObjectSO : ScriptableObject
	{
		public PlacementRule[] placementRules;

		public abstract bool CanPlace(TileObjectMap map, int x, int y);
	}
}
