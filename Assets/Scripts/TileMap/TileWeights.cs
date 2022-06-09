using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class TileWeights
	{
		[Range(-1f, 16f)]
		public float empty = 0;

		[Range(-1f, 16f)]
		public float full = 1;

		[Range(-1f, 16f)]
		public float edge = 0.5f;

		[Range(-1f, 16f)]
		public float smallCorner = 0.125f;

		[Range(-1f, 16f)]
		public float largeCorner = 0.875f;

		[Range(-1f, 16f)]
		public float diagonal = 0.75f;

		public float GetWeight(TileShape shape)
		{
			switch (shape)
			{
				case TileShape.SmallCorner:
					return smallCorner;

				case TileShape.LargeCorner:
					return largeCorner;

				case TileShape.Edge:
					return edge;

				case TileShape.Diagonal:
					return diagonal;

				case TileShape.Full:
					return full;

				case TileShape.Empty:
					return empty;

				default:
					return 0;
			}
		}

		public float GetWeight(Tile tile)
		{
			return GetWeight(tile.Shape());
		}
	}
}
