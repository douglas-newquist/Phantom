using System.Collections.Generic;
using System.Linq;
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
		public const float TileSize = 32;

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

		public RectInt Bounds => LevelDesign.TileLayerMap.Bounds;

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

		public bool IsSpawnable(Vector3Int coordinate)
		{
			if (LevelDesign.TileLayerMap.VertexTiles.Vertices.TryGet(coordinate.x, coordinate.y, out var v))
				return v == 0;
			return false;
		}

		public IEnumerable<Vector3Int> GetVertices(RectInt area)
		{
			for (int x = area.xMin; x < area.xMax; x++)
			{
				for (int y = area.yMin; y < area.yMax; y++)
				{
					var vertex = new Vector3Int(x, y);
					if (levelDesign.TileLayerMap.VertexTiles.Vertices.InBounds(x, y))
						yield return vertex;
				}
			}
		}

		public IEnumerable<Vector3Int> GetSpawnableVertices(RectInt area)
		{
			foreach (var vertex in GetVertices(area))
			{
				if (IsSpawnable(vertex))
					yield return vertex;
			}
		}

		public bool GetSpawnableVertex(RectInt area, out Vector3Int coordinate)
		{
			var spawnable = GetSpawnableVertices(area).ToArray();

			if (spawnable.Length == 0)
			{
				coordinate = Vector3Int.zero;
				return false;
			}

			coordinate = spawnable[Random.Range(0, spawnable.Length)];
			return true;
		}
	}
}
