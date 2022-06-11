using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.MapGenerator + "Sequence")]
	public class SequenceMapGenerator : MapGenerator
	{
		public MapGenerator[] generators;

		public override LevelDesign ApplyOnce(LevelDesign design, RectInt area)
		{
			foreach (var generator in generators)
				if (generator != null)
					design = generator.Apply(design, area);

			return design;
		}
	}
}
