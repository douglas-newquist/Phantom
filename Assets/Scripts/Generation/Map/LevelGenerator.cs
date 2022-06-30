using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.MapGenerator + "Level Generator")]
	public class LevelGenerator : ScriptableObject
	{
		[MinMax(1, Level.SizeLimit)]
		public IntRange width = new IntRange(Level.SizeLimit / 2, Level.SizeLimit);

		[MinMax(1, Level.SizeLimit)]
		public IntRange height = new IntRange(Level.SizeLimit / 2, Level.SizeLimit);

		public NameGenerator[] nameGenerators;

		public VertexGenerator[] vertexGenerators;

		public TileLayerMapGenerator[] tileLayerMapGenerators;

		public WeightedList<VertexTiles> textures;

		public LevelDesign Create()
		{
			return Create(width.Random, height.Random);
		}

		public LevelDesign Create(int width, int height)
		{
			var level = new LevelDesign(width, height);

			foreach (var generator in nameGenerators)
				level.Name = generator.Apply(level.Name);

			foreach (var generator in vertexGenerators)
				level.TileLayerMap.VertexTiles = generator.Apply(level.TileLayerMap.VertexTiles);

			foreach (var generator in tileLayerMapGenerators)
				level.TileLayerMap = generator.Apply(level.TileLayerMap);

			level.VertexTileTexture = textures.GetRandom();

			return level;
		}
	}
}
