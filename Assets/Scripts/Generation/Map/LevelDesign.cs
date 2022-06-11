using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class LevelDesign
	{
		public TileMapTexture tileMapTexture;

		public TileMap tiles;

		public List<LevelObject> objects = new List<LevelObject>();

		public int Width => tiles.Width;

		public int Height => tiles.Height;

		public LevelDesign(int width, int height)
		{
			tiles = new TileMap(width, height);
		}

		public GameObject Create()
		{
			var level = new GameObject();
			var renderer = level.AddComponent<SpriteRenderer>();
			renderer.sprite = tileMapTexture.DrawSprite(tiles);
			return level;
		}
	}
}
