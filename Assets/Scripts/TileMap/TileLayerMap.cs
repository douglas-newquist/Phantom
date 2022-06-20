using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[System.Serializable]
	public partial class TileLayerMap
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
		private List<TileLayer> layers = new List<TileLayer>();

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

			foreach (var layer in tileLayerMap.layers)
				layers.Add(new TileLayer(layer));
		}

		private int GetLayerIndex(int z)
		{
			for (int i = 0; i < layers.Count; i++)
				if (layers[i].z == z)
					return i;

			return -1;
		}

		public void SetLayer(int z, Grid2D<TileObject> tiles)
		{
			int i = GetLayerIndex(z);

			if (tiles.Size != Size)
				throw new System.InvalidOperationException("Tile layer size of " + tiles.Size + " is incompatible with " + Size + ".");

			if (i == -1)
			{
				layers.Add(new TileLayer(z, tiles));
			}
			else
				layers[i].tiles = tiles;
		}

		/// <summary>
		/// Gets a tile layer on this map
		/// </summary>
		/// <param name="z">What layer to get</param>
		/// <param name="autoCreate">If the layer didn't exist create one</param>
		public Grid2D<TileObject> GetLayer(int z, bool autoCreate = false)
		{
			int i = GetLayerIndex(z);

			if (i == -1)
			{
				if (autoCreate)
				{
					var layer = new Grid2D<TileObject>(Width, Height);
					SetLayer(z, layer);
					return layer;
				}

				return null;
			}

			return layers[i].tiles;
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
			var layer = GetLayer(position.z);
			if (layer == null)
				return null;
			return layer.Get(position.x, position.y);
		}

		/// <summary>
		/// Sets a tile on this tile map
		/// </summary>
		/// <param name="position">What position and layer to set</param>
		/// <param name="tile">Tile to assign to</param>
		public void SetTile(Vector3Int position, TileObject tile)
		{
			var layer = GetLayer(position.z, true);
			layer.Set(position.x, position.y, tile);
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

			foreach (var layer in layers)
			{
				for (int x = 0; x < Width; x++)
				{
					for (int y = 0; y < Height; y++)
					{
						var position = new Vector3Int(x, y, layer.z);
						var tile = GetTile(position);

						if (tile != null && tile.Tile != null)
							tilemap.SetTile(position, tile.Tile);
					}
				}
			}

			tilemap.CompressBounds();
		}
	}
}
