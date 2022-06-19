using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	/// <summary>
	/// Creates a GameObject from a TileObjectMap
	/// </summary>
	public abstract class TileLayerMapBuilder<T> : ScriptableObject
	{
		public GameObject prefab;

		public virtual GameObject Create(TileObjectMap map)
		{
			return null;
		}

		protected virtual void PlaceTiles(GameObject gameObject, TileLayerMap tileLayerMap, Tilemap tilemap)
		{

		}
	}
}
