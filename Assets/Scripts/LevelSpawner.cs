using Phantom.ObjectPooling;
using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Places the object on a spawnable tile
	/// </summary>
	public struct LevelSpawner : ISpawner
	{
		RectInt area;

		public LevelSpawner(RectInt area)
		{
			this.area = area;
		}

		public bool Spawn(GameObject obj)
		{
			if (area.width == 0 && area.height == 0)
				area = GameManager.CurrentLevel.Bounds;

			if (GameManager.CurrentLevel.GetSpawnableVertex(area, out var coordinate))
			{
				var pos = GameManager.CurrentLevel.GridToWorldPoint(coordinate);
				pos.x += Random.Range(0, Level.TileSize / 8);
				pos.y += Random.Range(0, Level.TileSize / 8);
				obj.transform.position = pos;
				return true;
			}

			return false;
		}
	}
}
