using UnityEngine;

namespace Phantom
{
	public abstract class MapGenerator : Generator<LevelDesign>
	{
		public GridGen tileGenerator;

		public override LevelDesign Apply(LevelDesign design)
		{
			var area = new RectInt(0, 0, design.Width, design.Height);
			return Apply(design, area);
		}

		public override LevelDesign Create(int width, int height)
		{
			var map = new LevelDesign(width, height);
			if (tileGenerator != null)
				map.tiles.vertices = tileGenerator.Apply(map.tiles.vertices);
			return Apply(map);
		}
	}
}
