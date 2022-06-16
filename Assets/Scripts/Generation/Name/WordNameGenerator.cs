using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.NameGenerator + "Random Word")]
	public class WordNameGenerator : NameGenerator
	{
		public string[] words;

		public override string ApplyOnce(string name)
		{
			if (words.Length == 0)
				return "";

			var chosen = words[Random.Range(0, words.Length)];

			if (string.IsNullOrEmpty(name))
				return chosen;

			return name + " " + chosen;
		}
	}
}
