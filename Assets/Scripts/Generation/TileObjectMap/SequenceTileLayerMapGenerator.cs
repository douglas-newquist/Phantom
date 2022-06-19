using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.TileLayerMapGenerator + "Sequence")]
	public class SequenceTileLayerMapGenerator : TileLayerMapGenerator
	{
		public TileLayerMapGenerator[] generators;

		public override TileLayerMap ApplyOnce(TileLayerMap design, RectInt area)
		{
			foreach (var generator in generators)
				if (generator != null)
					design = generator.Apply(design, area);

			return design;
		}
	}
}
