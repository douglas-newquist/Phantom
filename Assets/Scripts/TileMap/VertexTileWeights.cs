using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class VertexTileWeights
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

		public float GetWeight(VertexTileShape shape)
		{
			switch (shape)
			{
				case VertexTileShape.SmallCorner:
					return smallCorner;

				case VertexTileShape.LargeCorner:
					return largeCorner;

				case VertexTileShape.Edge:
					return edge;

				case VertexTileShape.Diagonal:
					return diagonal;

				case VertexTileShape.Full:
					return full;

				case VertexTileShape.Empty:
					return empty;

				default:
					return 0;
			}
		}

		public float GetWeight(VertexTile tile)
		{
			return GetWeight(tile.Shape());
		}
	}
}
