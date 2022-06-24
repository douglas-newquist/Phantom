using UnityEngine;
using UnityEngine.Tilemaps;

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

		[SerializeField]
		private LevelDesign levelDesign;

		public LevelDesign LevelDesign
		{
			get => levelDesign;
			set => levelDesign = value;
		}

		public Tilemap tilemap;

		public BoundsInt TileBounds => tilemap.cellBounds;

		public Rect WorldBounds
		{
			get
			{
				var min = GridToWorldPoint(TileBounds.min);
				var max = GridToWorldPoint(TileBounds.max);
				return new Rect(min, max - min);
			}
		}

		public bool AllowMiniMap => true;

		/// <summary>
		/// Converts a local tile grid coordinate to a world position
		/// </summary>
		public Vector3 GridToWorldPoint(Vector3Int coordinate)
		{
			return tilemap.CellToWorld(coordinate);
		}

		/// <summary>
		/// Converts a world position vector to a local tile gird coordinate
		/// </summary>
		public Vector3Int WorldToGridPoint(Vector3 position)
		{
			return tilemap.WorldToCell(position);
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(WorldBounds.center, WorldBounds.size);
		}
	}
}
