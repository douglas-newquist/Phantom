using UnityEngine;

namespace Phantom
{
	public abstract class WordGenerator : NameGenerator
	{
		public bool prepend = false;

		public string prefix, postfix;

		[Tooltip("Separator between this word generator and the current generated name")]
		public string separator = " ";

		[Tooltip("Separator between repeats in this generator")]
		public string wordSeparator = " ";

		public abstract string GetWord();

		public override string ApplyOnce(string name)
		{
			string word = prefix;
			bool first = true;

			for (int i = repeat.Random; i > 0; i--)
			{
				if (first)
				{
					word += GetWord();
					first = false;
				}
				else
					word += wordSeparator + GetWord();
			}

			word += postfix;

			if (string.IsNullOrEmpty(word))
				return name;

			if (string.IsNullOrEmpty(name))
				return word;

			if (prepend)
				return word + separator + name;

			return name + separator + word;
		}

		public override string Apply(string name)
		{
			return ApplyOnce(name);
		}
	}
}
