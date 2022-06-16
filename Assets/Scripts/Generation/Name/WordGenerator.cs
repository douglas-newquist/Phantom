namespace Phantom
{
	public abstract class WordGenerator : NameGenerator
	{
		public bool prepend = false;

		public string separator = " ";

		public abstract string GetWord();

		public override string ApplyOnce(string name)
		{
			var word = GetWord();

			if (string.IsNullOrEmpty(word))
				return name;

			if (prepend)
				return word + separator + name;

			return name + separator + word;
		}
	}
}
