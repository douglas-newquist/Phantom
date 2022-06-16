using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.NameGenerator + "Sequence")]
	public class SequenceNameGenerator : NameGenerator
	{
		public NameGenerator[] generators;

		public override string ApplyOnce(string name)
		{
			foreach (var generator in generators)
				if (generator != null)
					name = generator.Apply(name);

			return name;
		}
	}
}
