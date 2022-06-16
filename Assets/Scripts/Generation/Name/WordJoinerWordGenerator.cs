using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.NameGenerator + "Word Joiner")]
	public class WordJoinerWordGenerator : WordGenerator
	{
		public string subWordSeparator = "-";

		public WordGenerator[] words;

		public override string GetWord()
		{
			string word = "";

			for (int i = 0; i < words.Length; i++)
			{
				if (words[i] != null)
					word += words[i].Apply("");
				if (i < words.Length - 1)
					word += subWordSeparator;
			}

			return word;
		}
	}
}
