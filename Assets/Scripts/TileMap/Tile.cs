namespace Phantom
{
	//[System.Flags]
	public enum Tile
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

	public enum TileConnection { None, Solid, Air }

	public static class TileHelper
	{
		/// <summary>
		/// Gets the general shape of this tile
		/// </summary>
		public static TileShape Shape(this Tile tile)
		{
			switch (tile)
			{
				case Tile.BottomLeft:
				case Tile.BottomRight:
				case Tile.TopLeft:
				case Tile.TopRight:
					return TileShape.SmallCorner;

				case Tile.BottomLeftLarge:
				case Tile.BottomRightLarge:
				case Tile.TopLeftLarge:
				case Tile.TopRightLarge:
					return TileShape.LargeCorner;

				case Tile.BottomLeftTopRight:
				case Tile.TopLeftBottomRight:
					return TileShape.Diagonal;

				case Tile.Bottom:
				case Tile.Left:
				case Tile.Right:
				case Tile.Top:
					return TileShape.Edge;

				case Tile.Full:
					return TileShape.Full;

				case Tile.None:
					return TileShape.Empty;
			}

			return TileShape.None;
		}

		public static TileConnection ConnectionType(bool a, bool b)
		{
			if (a != b)
				return TileConnection.None;

			if (a) return TileConnection.Solid;
			return TileConnection.Air;
		}

		public static TileConnection Connectable(this Tile tile, Tile other, Direction direction)
		{
			TileConnection a = TileConnection.None, b = TileConnection.None;

			switch (direction)
			{
				case Direction.Up:
					a = ConnectionType(tile.HasFlag(Tile.TopLeft), other.HasFlag(Tile.BottomLeft));
					b = ConnectionType(tile.HasFlag(Tile.TopRight), other.HasFlag(Tile.BottomRight));
					break;

				case Direction.Right:
					a = ConnectionType(tile.HasFlag(Tile.TopRight), other.HasFlag(Tile.TopLeft));
					b = ConnectionType(tile.HasFlag(Tile.BottomRight), other.HasFlag(Tile.BottomLeft));
					break;

				case Direction.Down:
					a = ConnectionType(tile.HasFlag(Tile.BottomLeft), other.HasFlag(Tile.TopLeft));
					b = ConnectionType(tile.HasFlag(Tile.BottomRight), other.HasFlag(Tile.TopRight));
					break;

				case Direction.Left:
					a = ConnectionType(tile.HasFlag(Tile.TopLeft), other.HasFlag(Tile.TopRight));
					b = ConnectionType(tile.HasFlag(Tile.BottomLeft), other.HasFlag(Tile.BottomRight));
					break;
			}

			if (a == TileConnection.None || b == TileConnection.None)
				return TileConnection.None;
			if (a == TileConnection.Solid || b == TileConnection.Solid)
				return TileConnection.Solid;
			return TileConnection.Air;
		}

		public static Tile FlipY(this Tile tile)
		{
			Tile flipped = Tile.None;

			if (tile.HasFlag(Tile.BottomLeft))
				flipped |= Tile.BottomRight;
			if (tile.HasFlag(Tile.BottomRight))
				flipped |= Tile.BottomLeft;
			if (tile.HasFlag(Tile.TopLeft))
				flipped |= Tile.TopRight;
			if (tile.HasFlag(Tile.TopRight))
				flipped |= Tile.TopLeft;

			return flipped;
		}

		public static Tile FlipX(this Tile tile)
		{
			Tile flipped = Tile.None;

			if (tile.HasFlag(Tile.BottomLeft))
				flipped |= Tile.TopLeft;
			if (tile.HasFlag(Tile.BottomRight))
				flipped |= Tile.TopRight;
			if (tile.HasFlag(Tile.TopLeft))
				flipped |= Tile.BottomLeft;
			if (tile.HasFlag(Tile.TopRight))
				flipped |= Tile.BottomRight;

			return flipped;
		}

		public static Tile Invert(this Tile tile) => tile ^ Tile.Full;
	}
}
