using System;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class TileObjectMap : IGrid2D<Tuple<Tile, TileObject>>
	{
		[SerializeField]
		private TileMap tiles;

		public TileMap Tiles
		{
			get => tiles;
			set
			{
				if (value == null)
					tiles.Clear();
				else if (Size != value.Size)
					throw new System.InvalidOperationException("Tile map sizes do not match.");
				else
					tiles = value;
			}
		}

		[SerializeField]
		private Grid2D<TileObject> objects;

		public Grid2D<TileObject> Objects
		{
			get => objects;
			set
			{
				if (value == null)
					objects = new Grid2D<TileObject>(Width, Height, (x, y) => new TileObject());
				else if (Size != value.Size)
					throw new System.InvalidOperationException("Object map sizes do not match.");
				else
					objects = value;
			}
		}

		public int Width => Tiles.Width;

		public int Height => Tiles.Height;

		public Vector2Int Size => new Vector2Int(Width, Height);

		public RectInt BoundingBox => Tiles.BoundingBox;

		public TileObjectMap(int width, int height)
		{
			tiles = new TileMap(width, height);
			objects = new Grid2D<TileObject>(width, height, (x, y) => new TileObject());
		}

		public TileObjectMap(TileObjectMap map)
		{
			tiles = new TileMap(map.Tiles);
			objects = new Grid2D<TileObject>(map.Objects);
		}

		public virtual IGrid2D<Tuple<Tile, TileObject>> Clone()
		{
			return new TileObjectMap(this);
		}

		public Tile GetTile(int x, int y)
		{
			return Tiles.Get(x, y);
		}

		public TileObject GetTileObject(int x, int y)
		{
			return Objects.Get(x, y);
		}

		public Tuple<Tile, TileObject> Get(int x, int y)
		{
			return new Tuple<Tile, TileObject>(GetTile(x, y), GetTileObject(x, y));
		}

		public bool InBounds(int x, int y)
		{
			return Tiles.InBounds(x, y);
		}

		public void Set(int x, int y, Tuple<Tile, TileObject> value)
		{
			SetTile(x, y, value.Item1);
			SetObject(x, y, value.Item2);
		}

		public void SetTile(int x, int y, Tile value)
		{
			Tiles.Set(x, y, value);
		}

		public void SetObject(int x, int y, TileObject value)
		{
			Objects.Set(x, y, value);
		}

		public void Clear()
		{
			Tiles.Clear();

			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					GetTileObject(x, y).Clear();
		}
	}
}
