using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.NameGenerator + "Number")]
	public class NumberWordGenerator : WordGenerator
	{
		public IntRange range;

		public override string GetWord()
		{
			return range.Random.ToString();
		}
	}
}
