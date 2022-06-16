using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.TileObjectMapGenerator + "Sequence")]
	public class SequenceTileObjectMapGenerator : TileObjectMapGenerator
	{
		public TileObjectMapGenerator[] generators;

		public override TileObjectMap ApplyOnce(TileObjectMap design, RectInt area)
		{
			foreach (var generator in generators)
				if (generator != null)
					design = generator.Apply(design, area);

			return design;
		}
	}
}
