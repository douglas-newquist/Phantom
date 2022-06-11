using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.MapGenerator + "Selector")]
	public class SelectorMapGenerator : MapGenerator
	{
		public WeightedList<MapGenerator> generators;

		public override LevelDesign ApplyOnce(LevelDesign design, RectInt area)
		{
			var generator = generators.GetRandom();
			if (generator != null)
				return generator.Apply(design, area);
			return design;
		}
	}
}
