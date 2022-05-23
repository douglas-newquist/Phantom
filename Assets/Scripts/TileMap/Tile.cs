using UnityEngine;

namespace Game
{
	[System.Flags]
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

	public enum Direction { Up, Down, Left, Right }

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
			}

			return TileShape.None;
		}

		public static bool Connectable(this Tile tile, Tile other, Direction direction)
		{
			bool a = false, b = false;

			switch (direction)
			{
				case Direction.Up:
					a = tile.HasFlag(Tile.TopLeft) == other.HasFlag(Tile.BottomLeft);
					b = tile.HasFlag(Tile.TopRight) == other.HasFlag(Tile.BottomRight);
					return a && b;

				case Direction.Right:
					a = tile.HasFlag(Tile.TopRight) == other.HasFlag(Tile.TopLeft);
					b = tile.HasFlag(Tile.BottomRight) == other.HasFlag(Tile.BottomLeft);
					return a && b;

				case Direction.Down:
					a = tile.HasFlag(Tile.BottomLeft) == other.HasFlag(Tile.TopLeft);
					b = tile.HasFlag(Tile.BottomRight) == other.HasFlag(Tile.TopRight);
					return a && b;

				case Direction.Left:
					a = tile.HasFlag(Tile.TopLeft) == other.HasFlag(Tile.TopRight);
					b = tile.HasFlag(Tile.BottomLeft) == other.HasFlag(Tile.BottomRight);
					return a && b;
			}

			return false;
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
