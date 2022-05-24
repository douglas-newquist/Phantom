using UnityEngine;
namespace Game
{
	[CreateAssetMenu(menuName = "Game/Tile Map Texture")]
	public class TileMapTexture : ScriptableObject
	{
		public Sprite sprite;

		public int Width => sprite.texture.width / 4;

		public int Height => sprite.texture.height / 4;

		public Color[] GetTilePixels(Tile tile)
		{
			int x = (int)tile % 4;
			int y = (int)tile / 4;

			return sprite.texture.GetPixels(x * Width, y * Height, Width, Height);
		}

		public Texture2D Draw(TileMap map)
		{
			var texture = new Texture2D(Width * map.Width, Height * map.Height);
			Debug.Log(texture.width);
			DrawOverride(texture, map);
			return texture;
		}

		public void DrawOverride(Texture2D texture, TileMap map)
		{
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					var pixels = GetTilePixels(map.Get(x, y));
					texture.SetPixels(x * Width, y * Height, Width, Height, pixels);
				}
			}

			texture.Apply();
		}

		public Sprite DrawSprite(TileMap map)
		{
			var texture = Draw(map);
			var rect = new Rect(0, 0, texture.width, texture.height);
			var pivot = new Vector2(0.5f, 0.5f);
			var sprite = Sprite.Create(texture, rect, pivot, Width);
			System.IO.File.WriteAllBytes("test.png", sprite.texture.EncodeToPNG());
			return sprite;
		}
	}
}
