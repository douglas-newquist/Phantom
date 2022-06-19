using UnityEngine;

namespace Phantom
{
	public abstract class PlacementRule : ScriptableObject
	{
		public abstract bool CanPlace(MapTile obj, TileLayerMap map, Vector3Int position);
	}
}
