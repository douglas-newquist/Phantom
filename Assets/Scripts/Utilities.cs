using System.Collections.Generic;

namespace Phantom
{
	/// <summary>
	/// Stores random useful functions
	/// </summary>
	public static class Utilities
	{
		/// <summary>
		/// Swaps two variables
		/// </summary>
		public static void Swap<T>(ref T a, ref T b)
		{
			T tmp = a;
			a = b;
			b = tmp;
		}

		/// <summary>
		/// Swaps two values in the given list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">List to perform swap on</param>
		/// <param name="i1">Index of element 1</param>
		/// <param name="i2">Index of element 2</param>
		public static void Swap<T>(List<T> list, int i1, int i2)
		{
			T tmp = list[i1];
			list[i1] = list[i2];
			list[i2] = tmp;
		}

		public static void Swap<T>(T[] array, int i1, int i2)
		{
			T tmp = array[i1];
			array[i1] = array[i2];
			array[i2] = tmp;
		}
	}
}
