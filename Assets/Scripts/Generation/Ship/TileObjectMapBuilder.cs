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

		protected GameObject CreatePrefab()
		{
			if (prefab == null)
				return new GameObject("Tile Layer Map GameObject");
			return Instantiate(prefab);
		}

		public abstract GameObject Create(T design);
	}
}
