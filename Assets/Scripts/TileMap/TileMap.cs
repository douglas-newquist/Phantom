using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class TileMap : IGrid2D<Tile>
	{
		[SerializeField]
		private Grid2D<int> vertices;

		public Grid2D<int> Vertices
		{
			get => vertices;
			set
			{
				if (value == null)
					vertices.Clear();
				else
					vertices = value;
			}
		}

		/// <summary>
		/// How many tiles wide this map is
		/// </summary>
		public int Width => Vertices.Width - 1;

		/// <summary>
		/// How many tiles tall this map is
		/// </summary>
		public int Height => Vertices.Height - 1;

		public Vector2Int Size => new Vector2Int(Width, Height);

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
			Vertices = new Grid2D<int>(width + 1, height + 1);
		}

		public TileMap(Grid2D<int> vertices)
		{
			this.Vertices = vertices;
		}

		public TileMap(TileMap map) : this(map.Vertices) { }

		public bool InBounds(int x, int y)
		{
			return x >= 0 && x < Width && y >= 0 && y < Height;
		}

		public Tile Get(int x, int y)
		{
			Tile tile = Tile.None;

			if (Vertices.Get(x, y) > 0)
				tile |= Tile.BottomLeft;
			if (Vertices.Get(x + 1, y) > 0)
				tile |= Tile.BottomRight;
			if (Vertices.Get(x, y + 1) > 0)
				tile |= Tile.TopLeft;
			if (Vertices.Get(x + 1, y + 1) > 0)
				tile |= Tile.TopRight;

			return tile;
		}

		public void Set(int x, int y, Tile value)
		{
			if (value.HasFlag(Tile.BottomLeft))
				Vertices.Set(x, y, 1);
			if (value.HasFlag(Tile.BottomRight))
				Vertices.Set(x + 1, y, 1);
			if (value.HasFlag(Tile.TopLeft))
				Vertices.Set(x, y + 1, 1);
			if (value.HasFlag(Tile.TopRight))
				Vertices.Set(x + 1, y + 1, 1);
		}

		public IGrid2D<Tile> Clone()
		{
			return new TileMap(Vertices);
		}

		public void Clear()
		{
			Vertices.Clear();
		}
	}
}
