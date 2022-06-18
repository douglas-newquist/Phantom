namespace Phantom
{
	[System.Flags]
	public enum VertexTileShape
	{
		None = 0,
		Empty = 1,
		SmallCorner = 2,
		LargeCorner = 4,
		Corner = SmallCorner | LargeCorner,
		Edge = 8,
		Diagonal = 16,
		Full = 32,
		Border = Corner | Edge | Diagonal
	}
}
