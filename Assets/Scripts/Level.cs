using System.Collections.Generic;
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

		public TileLayerMap TileLayerMap => LevelDesign.TileLayerMap;

		public VertexTileMap VertexTileMap => TileLayerMap.VertexTiles;

		public Grid2D<int> Vertices => VertexTileMap.Vertices;

		public Vector3Int[] GetVerticesNear(Vector3 position)
		{
			var vertices = new Vector3Int[4];

			vertices[0] = WorldToGridPoint(position);
			vertices[1] = vertices[0];
			vertices[1].x++;
			vertices[2] = vertices[0];
			vertices[2].y++;
			vertices[3] = vertices[0];
			vertices[3].x++;
			vertices[3].y++;

			return vertices;
		}

		public Vector3Int GetClosestVertex(Vector3 position)
		{
			Vector3Int best = Vector3Int.zero;
			float bestDist = float.PositiveInfinity;

			foreach (var vertex in GetVerticesNear(position))
			{
				var pos = GridToWorldPoint(vertex);
				var dist = Vector2.Distance(pos, position);
				if (dist < bestDist)
				{
					best = vertex;
					bestDist = dist;
				}
			}

			return best;
		}

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
