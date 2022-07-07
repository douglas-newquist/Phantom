using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[CreateAssetMenu(menuName = "Game/Tiles/Map Tile")]
	public class MapTile : ScriptableObject
	{
		[SerializeField]
		[Range(1, 16)]
		private int width = 1;

		public int Width => width;

		[SerializeField]
		[Range(1, 16)]
		private int height = 1;

		public int Height => height;

		public TileBase tile;

		public PlacementRule[] placementRules;

		public virtual bool CanPlace(TileLayerMap map, Vector3Int position)
		{
			for (int xi = 0, x = position.x; xi < Width; xi++, x++)
			{
				for (int yi = 0, y = position.y; yi < Height; yi++, y++)
				{
					if (!map.InBounds(x, y))
						return false;

					if (xi != 0 || yi != 0)
					{
						var p = new Vector3Int(x, y, position.z);
						var tile = map.GetTile(p);
						if (tile != null && tile.Occupied)
							return false;
					}
				}
			}

			foreach (var rule in placementRules)
				if (rule != null && !rule.CanPlace(this, map, position))
					return false;

			return true;
		}

		public virtual void Place(TileLayerMap map, Vector3Int position)
		{
			map.SetTile(position, new TileObject(this));

			for (int xi = 0, x = position.x; xi < Width; xi++, x++)
				for (int yi = 0, y = position.y; yi < Height; yi++, y++)
					if (xi != 0 || yi != 0)
						map.SetTile(new Vector3Int(x, y, position.z), new TileObject(position));
		}

		public virtual void Place(Tilemap tilemap, Vector3Int position)
		{
			tilemap.SetTile(position, tile);
		}
	}
}
