using System;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class TileObjectMap : IGrid2D<Tuple<Tile, TileObject>>
	{
		public TileMap tiles;

		public Grid2D<TileObject> objects;

		public int Width => tiles.Width;

		public int Height => tiles.Height;

		public RectInt BoundingBox => tiles.BoundingBox;

		public TileObjectMap(int width, int height)
		{
			tiles = new TileMap(width, height);
			objects = new Grid2D<TileObject>(width, height, () => new TileObject());
		}

		public TileObjectMap(TileObjectMap map)
		{
			tiles = new TileMap(map.tiles);
			objects = new Grid2D<TileObject>(map.objects);
		}

		public virtual IGrid2D<Tuple<Tile, TileObject>> Clone()
		{
			return new TileObjectMap(this);
		}

		public Tuple<Tile, TileObject> Get(int x, int y)
		{
			return new Tuple<Tile, TileObject>(tiles.Get(x, y), objects.Get(x, y));
		}

		public bool InBounds(int x, int y)
		{
			return tiles.InBounds(x, y);
		}

		public void Set(int x, int y, Tuple<Tile, TileObject> value)
		{
			tiles.Set(x, y, value.Item1);
			objects.Set(x, y, value.Item2);
		}

		public void SetTile(int x, int y, Tile value)
		{
			tiles.Set(x, y, value);
		}

		public void SetObject(int x, int y, TileObject value)
		{
			objects.Set(x, y, value);
		}
	}
}
