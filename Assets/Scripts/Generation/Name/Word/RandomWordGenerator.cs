using UnityEngine;
using System.Linq;

namespace Phantom
{
	[ExecuteInEditMode]
	[CreateAssetMenu(menuName = CreateMenu.NameGenerator + "Random Word")]
	public class RandomWordGenerator : WordGenerator
	{
		public string[] words;

		public override string GetWord()
		{
			if (words.Length == 0)
				return "";

			return words[Random.Range(0, words.Length)].ToString();
		}

		private void OnEnable()
		{
			if (words != null)
				words = words.OrderBy((w) => w).ToArray();
		}

		private void OnGUI()
		{
			if (words != null)
				words = words.OrderBy((w) => w).ToArray();
		}
	}
}
