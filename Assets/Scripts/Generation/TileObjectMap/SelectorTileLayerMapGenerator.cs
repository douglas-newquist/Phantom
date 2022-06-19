using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.TileLayerMapGenerator + "Selector")]
	public class SelectorTileLayerMapGenerator : TileLayerMapGenerator
	{
		public WeightedList<TileLayerMapGenerator> generators;

		public override TileLayerMap ApplyOnce(TileLayerMap design, RectInt area)
		{
			var generator = generators.GetRandom();
			if (generator != null)
				return generator.Apply(design, area);
			return design;
		}
	}
}
