using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	public abstract class MapTile : TileBase
	{
		public abstract int Width { get; }

		public abstract int Height { get; }

		[SerializeField]
		private TileBase tile;

		public TileBase Tile { get => tile; set => tile = value; }

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

			for (int xi = 0, x = position.x; xi < Width; xi++, x++)
				for (int yi = 0, y = position.y; yi < Height; yi++, y++)
					if (xi != 0 || yi != 0)
						map.SetTile(new Vector3Int(x, y, position.z), new TileObject(position));
		}

		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			Tile.GetTileData(position, tilemap, ref tileData);
		}

		public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			Debug.Log(this.name);
			return Tile.StartUp(position, tilemap, go);
		}

		public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			return Tile.GetTileAnimationData(position, tilemap, ref tileAnimationData);
		}

		public override void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			Tile.RefreshTile(position, tilemap);
		}
	}
}
