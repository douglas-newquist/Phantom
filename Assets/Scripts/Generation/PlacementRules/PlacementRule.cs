using UnityEngine;

namespace Phantom
{
	public abstract class PlacementRule : ScriptableObject
	{
		public virtual bool CanPlace(MapTile tile, TileLayerMap map, Vector3Int position)
		{
			for (int x = position.x; x < position.x + tile.Width; x++)
			{
				for (int y = position.y; y < position.y + tile.Height; y++)
				{
					if (!map.InBounds(x, y))
						return false;

					var placed = map.GetTile(new Vector3Int(x, y, position.z));
					if (placed != null && placed.Occupied)
						return false;
				}
			}

			return true;
		}
	}
}
