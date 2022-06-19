using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Ship")]
	public class ShipGenerator : ScriptableObject
	{
		[MinMax(1, 32)]
		public IntRange width = new IntRange(24, 32);

		[MinMax(1, 32)]
		public IntRange height = new IntRange(24, 32);

		public NameGenerator[] nameGenerators;

		[Tooltip("Vertex generator to use if creating a new design")]
		public VertexGenerator[] vertexGenerators;

		public TileLayerMapGenerator[] tileLayerMapGenerators;

		public HullSelectorShipGenerator hullSelector;

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

			design.HullType = hullSelector.GetHull();

			return design;
		}
	}
}
