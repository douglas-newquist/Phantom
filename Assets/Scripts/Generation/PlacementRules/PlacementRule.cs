using UnityEngine;

namespace Phantom
{
	public abstract class PlacementRule : ScriptableObject
	{
		public abstract bool CanPlace(TileObjectSO obj, TileLayerMap map, Vector3Int position);
	}
}
