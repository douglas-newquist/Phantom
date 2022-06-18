using UnityEngine;

namespace Phantom
{
	public class Level : MonoBehaviour
	{
		/// <summary>
		/// Maximum width/height of levels
		/// </summary>
		public const int SizeLimit = 128;

		/// <summary>
		/// Units each tile in a tile occupies
		/// </summary>
		public const float TileSize = 64;

		/// <summary>
		/// Total number of units a level can occupy
		/// </summary>
		public const float TotalSizeLimit = TileSize * SizeLimit;

		/// <summary>
		/// Converts a local tile grid coordinate to a world position
		/// </summary>
		public Vector2 GridToWorldPoint(Vector2Int coordinate)
		{
			float x = coordinate.x * TileSize;
			float y = coordinate.y * TileSize;
			return new Vector2(x, y);
		}

		/// <summary>
		/// Converts a world position vector to a local tile gird coordinate
		/// </summary>
		public Vector2Int WorldToGridPoint(Vector3 position)
		{
			int x = Mathf.FloorToInt(position.x / TileSize);
			int y = Mathf.FloorToInt(position.y / TileSize);
			return new Vector2Int(x, y);
		}
	}
}
