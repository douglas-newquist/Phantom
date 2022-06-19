using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Ship")]
	public class ShipGenerator : ScriptableObject
	{
		[MinMax(1, ShipDesign.SizeLimit)]
		public IntRange width = new IntRange(24, ShipDesign.SizeLimit);

		[MinMax(1, ShipDesign.SizeLimit)]
		public IntRange height = new IntRange(24, ShipDesign.SizeLimit);

		public NameGenerator[] nameGenerators;

		[Tooltip("Vertex generator to use if creating a new design")]
		public VertexGenerator[] vertexGenerators;

		public TileLayerMapGenerator[] tileLayerMapGenerators;

		public WeightedList<TileMapSO> hulls;

		public ShipDesign Create()
		{
			return Create(width.Random, height.Random);
		}

		/// <summary>
		/// Creates a new ship design of the specified size
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public ShipDesign Create(int width, int height)
		{
			var design = new ShipDesign(width, height);

			foreach (var generator in nameGenerators)
				design.Name = generator.Apply(design.Name);

			foreach (var generator in vertexGenerators)
				design.TileLayerMap.Tiles = generator.Apply(design.TileLayerMap.Tiles);

			foreach (var generator in tileLayerMapGenerators)
				design.TileLayerMap = generator.Apply(design.TileLayerMap);

			design.HullType = hulls.GetRandom();

			return design;
		}
	}
}
