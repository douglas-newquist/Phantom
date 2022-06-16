using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.NameGenerator + "Roman Numeral")]
	public class RomanNumeralWordGenerator : WordGenerator
	{
		[MinMax(1, 100)]
		public IntRange range = new IntRange(1, 10);

		public override string GetWord()
		{
			return ToRoman(range.Random);
		}

		public string ToRoman(int n)
		{
			if (n >= 500) return "D" + ToRoman(n - 500);
			if (n >= 400) return "CD" + ToRoman(n - 400);
			if (n >= 100) return "C" + ToRoman(n - 100);
			if (n >= 90) return "XC" + ToRoman(n - 90);
			if (n >= 50) return "L" + ToRoman(n - 50);
			if (n >= 40) return "XL" + ToRoman(n - 40);
			if (n >= 10) return "X" + ToRoman(n - 10);
			if (n >= 9) return "IX" + ToRoman(n - 9);
			if (n >= 5) return "V" + ToRoman(n - 5);
			if (n >= 4) return "IV" + ToRoman(n - 4);
			if (n >= 1) return "I" + ToRoman(n - 1);
			if (n == 0) return "";

			throw new System.ArgumentOutOfRangeException("Roman numeral for " + n + " does not exist.");
		}
	}
}
