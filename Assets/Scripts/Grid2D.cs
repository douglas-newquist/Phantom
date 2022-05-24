using UnityEngine;
namespace Game
{
	[System.Serializable]
	public class Grid2D<T> : IGrid2D<T>
	{
		[SerializeField]
		private int width, height;

		/// <summary>
		/// Gets the width of this grid
		/// </summary>
		public int Width => width;

		/// <summary>
		/// Gets the height of this grid
		/// </summary>
		public int Height => height;

		[SerializeField]
		private T[] values;

		public Grid2D(int width, int height)
		{
			this.width = width;
			this.height = height;

			values = new T[width * height];
		}

		/// <summary>
		/// Checks if the given coordinate is inside this grid
		/// </summary>
		public virtual bool InBounds(int x, int y)
		{
			return x >= 0 && x < Width && y >= 0 && y < Height;
		}

		/// <summary>
		/// Gets the value at the given coordinate
		/// </summary>
		public virtual T Get(int x, int y)
		{
			return values[x * width + y];
		}

		/// <summary>
		/// Sets the value of a given coordinate
		/// </summary>
		public virtual void Set(int x, int y, T value)
		{
			values[x * width + y] = value;
		}
	}
}
