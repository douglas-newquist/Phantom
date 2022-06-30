using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class LevelDesign
	{
		[SerializeField]
		private string name;

		public string Name
		{
			get => name;
			set => name = value;
		}

		[SerializeField]
		private TileLayerMap tileLayerMap;

		public TileLayerMap TileLayerMap
		{
			get => tileLayerMap;
			set => tileLayerMap = value;
		}

		[SerializeField]
		private VertexTiles tileMapTexture;

		public VertexTiles VertexTileTexture
		{
			get => TileLayerMap.VertexTileTiles;
			set => TileLayerMap.VertexTileTiles = value;
		}

		private List<LevelObject> specialObjects = new List<LevelObject>();

		public List<LevelObject> SpecialObjects
		{
			get => specialObjects;
			set => specialObjects = value;
		}

		public LevelDesign(int width, int height)
		{
			TileLayerMap = new TileLayerMap(width, height);
		}

		public LevelDesign(LevelDesign levelDesign)
		{
			TileLayerMap = new TileLayerMap(levelDesign.TileLayerMap);
			SpecialObjects = new List<LevelObject>(levelDesign.SpecialObjects);
			Name = levelDesign.Name;
		}
	}
}
