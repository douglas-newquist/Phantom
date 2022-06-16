using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.NameGenerator + "Selector")]
	public class SelectorNameGenerator : NameGenerator
	{
		public WeightedList<NameGenerator> generators;

		public override string ApplyOnce(string name)
		{
			var generator = generators.GetRandom();
			if (generator != null)
				return generator.Apply(name);
			return name;
		}
	}
}
