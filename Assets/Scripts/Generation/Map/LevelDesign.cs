using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class LevelDesign : TileObjectMap
	{
		public TileMapTexture tileMapTexture;

		public LevelDesign(int width, int height) : base(width, height)
		{
		}

		public LevelDesign(TileObjectMap map) : base(map) { }

		public LevelDesign(LevelDesign level) : base(level)
		{
		}

		public GameObject Create()
		{
			var level = new GameObject();
			var renderer = level.AddComponent<SpriteRenderer>();
			renderer.sprite = tileMapTexture.DrawSprite(Tiles);

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					var part = Get(x, y).Item2;
					if (part.State == Reservation.Used)
					{
						var placed = part.Object.Place(level, this, x, y);
						if (placed != null)
						{
							placed.transform.SetParent(level.transform);
							placed.transform.localPosition = new Vector3(x, y, 0);
						}
					}
				}
			}

			level.transform.localScale = new Vector3(32, 32, 1);

			return level;
		}

		public override IGrid2D<Tuple<Tile, TileObject>> Clone()
		{
			return new LevelDesign(this);
		}
	}
}
