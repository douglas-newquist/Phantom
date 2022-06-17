using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Creates a GameObject from a TileObjectMap
	/// </summary>
	public abstract class TileObjectMapBuilder : ScriptableObject
	{
		public GameObject prefab;

		public virtual GameObject Create(TileObjectMap map)
		{
			GameObject obj;
			if (prefab != null)
				obj = Instantiate(prefab);
			else
				obj = new GameObject("Tile Map");

			var tiles = new GameObject("Tiles");
			tiles.transform.SetParent(obj.transform);
			tiles.transform.localPosition = Vector3.zero;
			PlaceTiles(obj, map, tiles);

			var objects = new GameObject("Objects");
			objects.transform.SetParent(obj.transform);
			objects.transform.localPosition = Vector3.zero;
			PlaceTileObjects(obj, map, objects);

			return obj;
		}

		protected abstract void PlaceTiles(GameObject gameObject, TileObjectMap map, GameObject container);

		protected virtual void PlaceTileObjects(GameObject gameObject, TileObjectMap map, GameObject container)
		{
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					var obj = map.Get(x, y).Item2;
					if (obj.State == Reservation.Used)
					{
						obj.Object.Place(gameObject, map, x, y, container.transform);
					}
				}
			}
		}
	}
}
