using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[System.Serializable]
	public class TileLayerMap
	{
		[SerializeField]
		private VertexTiles vertexTileTiles;

		/// <summary>
		/// Set of tile definitions for TileBases foreach vertex tile
		/// </summary>
		public VertexTiles VertexTileTiles
		{
			get => vertexTileTiles;
			set => vertexTileTiles = value;
		}

		[SerializeField]
		private VertexTileMap vertexTiles;

		/// <summary>
		/// The vertex based tile layer
		/// </summary>
		public VertexTileMap Tiles
		{
			get => vertexTiles;
			set
			{
				if (value == null)
					vertexTiles.Clear();
				else if (value.Size != Size)
					throw new System.InvalidOperationException("Foreground tile layer size " + value.Size + " is incompatible with " + Size + ".");
				else
					vertexTiles = value;
			}
		}

		[SerializeField]
		private SerialDictionary<Vector3Int, TileObject> tiles = new SerialDictionary<Vector3Int, TileObject>();

		/// <summary>
		/// Number of tiles wide this map is
		/// </summary>
		public int Width => Tiles.Width;

		/// <summary>
		/// Number tile tall this map is
		/// </summary>
		public int Height => Tiles.Height;

		/// <summary>
		/// Size in tiles of this map
		/// </summary>
		public Vector2Int Size => Tiles.Size;

		/// <summary>
		/// Creates a blank map of the given size
		/// </summary>
		public TileLayerMap(int width, int height)
		{
			vertexTiles = new VertexTileMap(width, height);
		}

		/// <summary>
		/// Creates a new layer map using the given vertex tiles
		/// </summary>
		public TileLayerMap(VertexTileMap vertexTiles)
		{
			this.vertexTiles = vertexTiles;
		}

		/// <summary>
		/// Creates a deep copy of this map
		/// </summary>
		public TileLayerMap(TileLayerMap tileLayerMap)
		{
			vertexTiles = new VertexTileMap(tileLayerMap.Tiles);
			vertexTileTiles = tileLayerMap.VertexTileTiles;

			foreach (var tile in tileLayerMap.tiles)
				tiles[tile.Key] = tile.Value;
		}

		public bool InBounds(int x, int y)
		{
			return Tiles.InBounds(x, y);
		}

		/// <summary>
		/// Gets a tile on the layer map
		/// </summary>
		/// <param name="position">What position and layer to get</param>
		/// <returns></returns>
		public TileObject GetTile(Vector3Int position)
		{
			if (tiles.TryGetValue(position, out var tile))
				return tile;
			return null;
		}

		/// <summary>
		/// Sets a tile on this tile map
		/// </summary>
		/// <param name="position">What position and layer to set</param>
		/// <param name="tile">Tile to assign to</param>
		public void SetTile(Vector3Int position, TileObject tile)
		{
			tiles[position] = tile;
		}

		public void AddTiles(GameObject root, Tilemap tilemap)
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					var position = new Vector3Int(x, y, 0);
					if (Tiles.TryGet(x, y, out var tile))
						tilemap.SetTile(position, VertexTileTiles.GetTile(tile));
				}
			}

			foreach (var keyValuePair in tiles)
			{
				var position = keyValuePair.Key;
				var tile = keyValuePair.Value;

				if (tile != null && tile.Tile != null)
					tilemap.SetTile(position, tile.Tile);
			}

			tilemap.CompressBounds();
		}
	}
}
