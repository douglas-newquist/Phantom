using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.TileObjectMapGenerator + "Selector")]
	public class SelectorTileObjectMapGenerator : TileObjectMapGenerator
	{
		public WeightedList<TileObjectMapGenerator> generators;

		public override TileObjectMap ApplyOnce(TileObjectMap design, RectInt area)
		{
			var generator = generators.GetRandom();
			if (generator != null)
				return generator.Apply(design, area);
			return design;
		}
	}
}
