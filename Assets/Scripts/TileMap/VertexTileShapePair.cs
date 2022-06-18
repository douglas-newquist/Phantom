using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class VertexTileShapePair<T>
	{
		public T empty, full, edge, smallCorner, largeCorner, diagonal;

		public T Get(VertexTileShape shape)
		{
			switch (shape)
			{
				case VertexTileShape.Empty:
					return empty;

				case VertexTileShape.Full:
					return full;

				case VertexTileShape.Edge:
					return edge;

				case VertexTileShape.SmallCorner:
					return smallCorner;

				case VertexTileShape.LargeCorner:
					return largeCorner;

				case VertexTileShape.Diagonal:
					return diagonal;

				default:
					Debug.LogError("Unknown Vertex Tile Shape " + shape);
					return default(T);
			}
		}

		public T Get(VertexTile tile)
		{
			return Get(tile.Shape());
		}
	}
}
