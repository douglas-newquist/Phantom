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
	}
}
