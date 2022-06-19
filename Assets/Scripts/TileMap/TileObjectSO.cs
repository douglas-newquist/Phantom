using UnityEngine;

namespace Phantom
{
	public abstract class TileObjectSO : ScriptableObject
	{
		public abstract int Width { get; }

		public abstract int Height { get; }

		public PlacementRule[] placementRules;

		public virtual bool CanPlace(TileLayerMap map, Vector3Int position)
		{
			foreach (var rule in placementRules)
				if (rule != null && !rule.CanPlace(this, map, position))
					return false;

			return true;
		}

		public virtual void Place(TileLayerMap map, Vector3Int position)
		{
			map.SetTile(position, new TileObject(this));

			for (int xi = 0; xi < Width; xi++)
				for (int yi = 0; yi < Height; yi++)
					if (xi != 0 || yi != 0)
						map.SetTile(position, new TileObject(position));
		}

		public abstract GameObject Place(GameObject obj, TileLayerMap map, int x, int y, Transform container);
	}
}
