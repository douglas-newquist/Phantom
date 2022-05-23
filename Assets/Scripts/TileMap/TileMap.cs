using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class TileMap
	{
		[SerializeField]
		private int width, height;

		/// <summary>
		/// How many tiles wide this map is
		/// </summary>
		public int TilesWidth => width;

		/// <summary>
		/// How many tiles tall this map is
		/// </summary>
		public int TilesHeight => height;

		/// <summary>
		/// How many vertices wide this map is
		/// </summary>
		public int VertexWidth => width + 1;

		/// <summary>
		/// How many vertices tall this map is
		/// </summary>
		public int VertexHeight => height + 1;

		[SerializeField]
		private int[] vertices;

		public TileMap(int width, int height)
		{
			this.width = width;
			this.height = height;

			vertices = new int[VertexWidth * VertexHeight];
		}

		/// <summary>
		/// Checks if the given coordinate is a valid vertex coordinate
		/// </summary>
		public bool InVertexBounds(int x, int y)
		{
			return x >= 0 && x < VertexWidth && y >= 0 && y < VertexHeight;
		}

		/// <summary>
		/// Checks if the given coordinate is a valid tile coordinate
		/// </summary>
		public bool InTileBounds(int x, int y)
		{
			return x >= 0 && x < TilesWidth && y >= 0 && y < TilesHeight;
		}

		public int GetVertex(int x, int y)
		{
			if (InVertexBounds(x, y))
				return vertices[x * width + y];
			return -1;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="value">Value to set the vertex to</param>
		public void SetVertex(int x, int y, int value)
		{
			if (InVertexBounds(x, y))
				vertices[x * width + y] = value;
		}

		public Tile GetTile(int x, int y)
		{
			Tile tile = Tile.None;

			if (GetVertex(x, y) > 0)
				tile |= Tile.BottomLeft;
			if (GetVertex(x + 1, y) > 0)
				tile |= Tile.BottomRight;
			if (GetVertex(x, y + 1) > 0)
				tile |= Tile.TopLeft;
			if (GetVertex(x + 1, y + 1) > 0)
				tile |= Tile.TopRight;

			return tile;
		}

		public void SetTile(int x, int y, Tile tile)
		{
			if (tile.HasFlag(Tile.BottomLeft))
				SetVertex(x, y, 1);
			if (tile.HasFlag(Tile.BottomRight))
				SetVertex(x + 1, y, 1);
			if (tile.HasFlag(Tile.TopLeft))
				SetVertex(x, y + 1, 1);
			if (tile.HasFlag(Tile.TopRight))
				SetVertex(x + 1, y + 1, 1);
		}
	}
}
