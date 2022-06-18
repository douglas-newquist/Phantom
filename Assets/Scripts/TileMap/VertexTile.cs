namespace Phantom
{
	public enum VertexTile
	{
		None = 0,

		// Single vertex tiles
		BottomLeft = 1,
		BottomRight = 2,
		TopLeft = 4,
		TopRight = 8,

		// Two vertex tiles
		Bottom = BottomLeft | BottomRight,
		Left = BottomLeft | TopLeft,
		Right = BottomRight | TopRight,
		Top = TopLeft | TopRight,

		TopLeftBottomRight = TopLeft | BottomRight,
		BottomLeftTopRight = BottomLeft | TopRight,

		// Three vertex tiles
		BottomLeftLarge = Bottom | Left,
		BottomRightLarge = Bottom | Right,
		TopLeftLarge = Left | Top,
		TopRightLarge = Right | Top,

		// Four vertex tiles
		Full = BottomLeft | BottomRight | TopLeft | TopRight
	}

	public enum VertexTileConnection { None, Solid, Air }

	public static class VertexTileHelper
	{
		/// <summary>
		/// Gets the general shape of this tile
		/// </summary>
		public static VertexTileShape Shape(this VertexTile tile)
		{
			switch (tile)
			{
				case VertexTile.BottomLeft:
				case VertexTile.BottomRight:
				case VertexTile.TopLeft:
				case VertexTile.TopRight:
					return VertexTileShape.SmallCorner;

				case VertexTile.BottomLeftLarge:
				case VertexTile.BottomRightLarge:
				case VertexTile.TopLeftLarge:
				case VertexTile.TopRightLarge:
					return VertexTileShape.LargeCorner;

				case VertexTile.BottomLeftTopRight:
				case VertexTile.TopLeftBottomRight:
					return VertexTileShape.Diagonal;

				case VertexTile.Bottom:
				case VertexTile.Left:
				case VertexTile.Right:
				case VertexTile.Top:
					return VertexTileShape.Edge;

				case VertexTile.Full:
					return VertexTileShape.Full;

				case VertexTile.None:
					return VertexTileShape.Empty;
			}

			return VertexTileShape.None;
		}

		public static VertexTileConnection ConnectionType(bool a, bool b)
		{
			if (a != b)
				return VertexTileConnection.None;

			if (a) return VertexTileConnection.Solid;
			return VertexTileConnection.Air;
		}

		public static VertexTileConnection Connectable(this VertexTile tile, VertexTile other, Direction direction)
		{
			VertexTileConnection a = VertexTileConnection.None, b = VertexTileConnection.None;

			switch (direction)
			{
				case Direction.Up:
					a = ConnectionType(tile.HasFlag(VertexTile.TopLeft), other.HasFlag(VertexTile.BottomLeft));
					b = ConnectionType(tile.HasFlag(VertexTile.TopRight), other.HasFlag(VertexTile.BottomRight));
					break;

				case Direction.Right:
					a = ConnectionType(tile.HasFlag(VertexTile.TopRight), other.HasFlag(VertexTile.TopLeft));
					b = ConnectionType(tile.HasFlag(VertexTile.BottomRight), other.HasFlag(VertexTile.BottomLeft));
					break;

				case Direction.Down:
					a = ConnectionType(tile.HasFlag(VertexTile.BottomLeft), other.HasFlag(VertexTile.TopLeft));
					b = ConnectionType(tile.HasFlag(VertexTile.BottomRight), other.HasFlag(VertexTile.TopRight));
					break;

				case Direction.Left:
					a = ConnectionType(tile.HasFlag(VertexTile.TopLeft), other.HasFlag(VertexTile.TopRight));
					b = ConnectionType(tile.HasFlag(VertexTile.BottomLeft), other.HasFlag(VertexTile.BottomRight));
					break;
			}

			if (a == VertexTileConnection.None || b == VertexTileConnection.None)
				return VertexTileConnection.None;
			if (a == VertexTileConnection.Solid || b == VertexTileConnection.Solid)
				return VertexTileConnection.Solid;
			return VertexTileConnection.Air;
		}

		public static VertexTile FlipY(this VertexTile tile)
		{
			VertexTile flipped = VertexTile.None;

			if (tile.HasFlag(VertexTile.BottomLeft))
				flipped |= VertexTile.BottomRight;
			if (tile.HasFlag(VertexTile.BottomRight))
				flipped |= VertexTile.BottomLeft;
			if (tile.HasFlag(VertexTile.TopLeft))
				flipped |= VertexTile.TopRight;
			if (tile.HasFlag(VertexTile.TopRight))
				flipped |= VertexTile.TopLeft;

			return flipped;
		}

		public static VertexTile FlipX(this VertexTile tile)
		{
			VertexTile flipped = VertexTile.None;

			if (tile.HasFlag(VertexTile.BottomLeft))
				flipped |= VertexTile.TopLeft;
			if (tile.HasFlag(VertexTile.BottomRight))
				flipped |= VertexTile.TopRight;
			if (tile.HasFlag(VertexTile.TopLeft))
				flipped |= VertexTile.BottomLeft;
			if (tile.HasFlag(VertexTile.TopRight))
				flipped |= VertexTile.BottomRight;

			return flipped;
		}

		public static VertexTile Invert(this VertexTile tile) => tile ^ VertexTile.Full;
	}
}
