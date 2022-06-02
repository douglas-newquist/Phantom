using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class TileMap : IGrid2D<Tile>
	{
		public Grid2D<int> vertices;

		/// <summary>
		/// How many tiles wide this map is
		/// </summary>
		public int Width => vertices.Width - 1;

		/// <summary>
		/// How many tiles tall this map is
		/// </summary>
		public int Height => vertices.Height - 1;

		/// <summary>
		/// Gets the area in which all tiles other than Tile.None are contained
		/// </summary>
		public RectInt BoundingBox
		{
			get
			{
				int xMin = Width - 1, xMax = 0;
				int yMin = Height - 1, yMax = 0;

				for (int x = 0; x < Width; x++)
				{
					for (int y = 0; y < Height; y++)
					{
						if (Get(x, y) != Tile.None)
						{
							xMin = Mathf.Min(xMin, x);
							xMax = Mathf.Max(xMax, x);
							yMin = Mathf.Min(yMin, y);
							yMax = Mathf.Max(yMax, y);
						}
					}
				}

				if (xMin > xMax || yMin > yMax)
					return new RectInt(0, 0, 0, 0);

				return new RectInt(xMin, yMin, xMax - xMin, yMax - yMin);
			}
		}

		public TileMap(int width, int height)
		{
			vertices = new Grid2D<int>(width + 1, height + 1);
		}

		public TileMap(Grid2D<int> vertices)
		{
			this.vertices = vertices;
		}

		public TileMap(TileMap map) : this(map.vertices) { }

		public bool InBounds(int x, int y)
		{
			return x >= 0 && x < Width && y >= 0 && y < Height;
		}

		public Tile Get(int x, int y)
		{
			Tile tile = Tile.None;

			if (vertices.Get(x, y) > 0)
				tile |= Tile.BottomLeft;
			if (vertices.Get(x + 1, y) > 0)
				tile |= Tile.BottomRight;
			if (vertices.Get(x, y + 1) > 0)
				tile |= Tile.TopLeft;
			if (vertices.Get(x + 1, y + 1) > 0)
				tile |= Tile.TopRight;

			return tile;
		}

		public void Set(int x, int y, Tile value)
		{
			if (value.HasFlag(Tile.BottomLeft))
				vertices.Set(x, y, 1);
			if (value.HasFlag(Tile.BottomRight))
				vertices.Set(x + 1, y, 1);
			if (value.HasFlag(Tile.TopLeft))
				vertices.Set(x, y + 1, 1);
			if (value.HasFlag(Tile.TopRight))
				vertices.Set(x + 1, y + 1, 1);
		}

		public IGrid2D<Tile> Clone()
		{
			return new TileMap(vertices);
		}
	}
}
