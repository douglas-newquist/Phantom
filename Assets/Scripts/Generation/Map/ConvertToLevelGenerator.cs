using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.MapGenerator + "Convert to Level")]
	public class ConvertToLevelGenerator : TileObjectMapGenerator
	{
		public override TileObjectMap ApplyOnce(TileObjectMap design, RectInt area)
		{
			return new LevelDesign(design);
		}

		public override TileObjectMap Create(int width, int height)
		{
			var level = new LevelDesign(width, height);
			if (tileGenerator != null)
				level.Tiles = tileGenerator.Apply(level.Tiles);
			return Apply(level);
		}
	}
}
