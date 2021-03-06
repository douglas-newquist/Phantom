using System;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class TileObjectMap : IGrid2D<Tuple<VertexTile, TileObject>>
	{
		[SerializeField]
		private VertexTileMap tiles;

		public VertexTileMap Tiles
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

		public RectInt Bounds => new RectInt(0, 0, Width, Height);

		public Vector2Int Size => new Vector2Int(Width, Height);

		public RectInt BoundingBox => Tiles.BoundingBox;

		public TileObjectMap(int width, int height)
		{
			tiles = new VertexTileMap(width, height);
			objects = new Grid2D<TileObject>(width, height, (x, y) => new TileObject());
		}

		public TileObjectMap(TileObjectMap map)
		{
			tiles = new VertexTileMap(map.Tiles);
			objects = new Grid2D<TileObject>(map.Objects);
		}

		public virtual IGrid2D<Tuple<VertexTile, TileObject>> Clone()
		{
			return new TileObjectMap(this);
		}

		public VertexTile GetTile(int x, int y)
		{
			return Tiles.Get(x, y);
		}

		public TileObject GetTileObject(int x, int y)
		{
			return Objects.Get(x, y);
		}

		public Tuple<VertexTile, TileObject> Get(int x, int y)
		{
			return new Tuple<VertexTile, TileObject>(GetTile(x, y), GetTileObject(x, y));
		}

		public bool InBounds(int x, int y)
		{
			return Tiles.InBounds(x, y);
		}

		public void Set(int x, int y, Tuple<VertexTile, TileObject> value)
		{
			SetTile(x, y, value.Item1);
			SetObject(x, y, value.Item2);
		}

		public void SetTile(int x, int y, VertexTile value)
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

		public bool TryGet(int x, int y, out Tuple<VertexTile, TileObject> value)
		{
			throw new NotImplementedException();
		}

		public bool TrySet(int x, int y, Tuple<VertexTile, TileObject> value)
		{
			throw new NotImplementedException();
		}
	}
}
