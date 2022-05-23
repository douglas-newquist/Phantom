namespace Game
{
	[System.Flags]
	public enum TileShape
	{
		None = 0,
		SmallCorner = 1,
		LargeCorner = 2,
		Corner = SmallCorner | LargeCorner,
		Edge = 4,
		Diagonal = 8,
		Full = 16
	}
}