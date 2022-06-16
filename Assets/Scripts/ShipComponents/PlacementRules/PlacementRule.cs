using UnityEngine;

namespace Phantom
{
	public abstract class PlacementRule : ScriptableObject
	{
		public abstract bool CanPlace(TileObjectSO obj, TileObjectMap map, int x, int y);
	}
}
