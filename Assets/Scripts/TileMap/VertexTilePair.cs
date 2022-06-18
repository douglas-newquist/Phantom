using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class VertexTilePair<T>
	{
		[Header("Full tiles")]
		public T full;
		public T empty;

		[Header("Edge Tiles")]
		public T left;
		public T right, top, bottom;

		[Header("Small Corner Tiles")]
		public T bottomLeft;
		public T bottomRight, topLeft, topRight;

		[Header("Large Corner Tiles")]
		public T bottomLeftLarge;
		public T bottomRightLarge, topLeftLarge, topRightLarge;

		[Header("Diagonal Tiles")]
		public T bottomLeftTopRight;
		public T topLeftBottomRight;

		public T Get(VertexTile tile)
		{
			switch (tile)
			{
				case VertexTile.None:
					return empty;
				case VertexTile.Full:
					return full;

				case VertexTile.Left:
					return left;
				case VertexTile.Right:
					return right;
				case VertexTile.Top:
					return top;
				case VertexTile.Bottom:
					return bottom;

				case VertexTile.BottomLeft:
					return bottomLeft;
				case VertexTile.BottomRight:
					return bottomRight;
				case VertexTile.TopLeft:
					return topLeft;
				case VertexTile.TopRight:
					return topRight;

				case VertexTile.BottomLeftLarge:
					return bottomLeftLarge;
				case VertexTile.BottomRightLarge:
					return bottomRightLarge;
				case VertexTile.TopLeftLarge:
					return topLeftLarge;
				case VertexTile.TopRightLarge:
					return topRightLarge;

				case VertexTile.BottomLeftTopRight:
					return bottomLeftTopRight;
				case VertexTile.TopLeftBottomRight:
					return topLeftBottomRight;

				default:
					Debug.LogError("Unknown Vertex Tile " + tile);
					return default(T);
			}
		}
	}
}
