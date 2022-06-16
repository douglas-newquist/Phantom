using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.NameGenerator + "Letter")]
	public class LetterWordGenerator : WordGenerator
	{
		[TextArea]
		public string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public override string GetWord()
		{
			return letters[Random.Range(0, letters.Length)].ToString();
		}
	}
}
