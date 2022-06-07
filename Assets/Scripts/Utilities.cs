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
	}
}
