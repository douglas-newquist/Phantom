using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.MapGenerator + "Level Generator")]
	public class LevelGenerator : ScriptableObject
	{
		[MinMax(1, Level.SizeLimit)]
		public IntRange width = new IntRange(Level.SizeLimit / 2, Level.SizeLimit);

		public NameGenerator[] nameGenerators;

		public VertexGenerator[] vertexGenerators;

		public TileLayerMapGenerator[] tileLayerMapGenerators;

		public LevelDesign Create(int width, int height)
		{
			var level = new LevelDesign(width, height);

			foreach (var generator in nameGenerators)
				level.Name = generator.Apply(level.Name);

			foreach (var generator in vertexGenerators)
				level.TileLayerMap.Tiles = generator.Apply(level.TileLayerMap.Tiles);

			foreach (var generator in tileLayerMapGenerators)
				level.TileLayerMap = generator.Apply(level.TileLayerMap);

			return level;
		}
	}
}
