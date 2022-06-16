using UnityEngine;

namespace Phantom
{
	public abstract class TileObjectSO : ScriptableObject
	{
		public abstract int Width { get; }

		public abstract int Height { get; }

		public PlacementRule[] placementRules;

		public virtual bool CanPlace(TileObjectMap map, int x, int y)
		{
			foreach (var rule in placementRules)
				if (!rule.CanPlace(this, map, x, y))
					return false;

			return true;
		}

		public abstract void Place(TileObjectMap map, int x, int y);

		public abstract GameObject Place(GameObject obj, TileObjectMap map, int x, int y);
	}
}
