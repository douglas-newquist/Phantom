using UnityEngine;

namespace Phantom
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

		public Vector2Int Size => new Vector2Int(Width, Height);

		[SerializeField]
		private T[] values;

		public int Length => values.Length;

		public Grid2D(int width, int height)
		{
			this.width = width;
			this.height = height;

			values = new T[width * height];
		}

		public Grid2D(int width, int height, T value) : this(width, height)
		{
			for (int i = 0; i < values.Length; i++)
				values[i] = value;
		}

		public Grid2D(int width, int height, System.Func<int, int, T> creator) : this(width, height)
		{
			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					Set(x, y, creator(x, y));
		}

		public Grid2D(Grid2D<T> grid) : this(grid.Width, grid.Height)
		{
			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					Set(x, y, grid.Get(x, y));
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

		public virtual bool TryGet(int x, int y, out T value)
		{
			if (InBounds(x, y))
			{
				value = Get(x, y);
				return true;
			}

			value = default(T);
			return false;
		}

		public virtual T Get(int index)
		{
			return values[index];
		}

		/// <summary>
		/// Sets the value of a given coordinate
		/// </summary>
		public virtual void Set(int x, int y, T value)
		{
			values[x * width + y] = value;
		}

		public virtual bool TrySet(int x, int y, T value)
		{
			if (InBounds(x, y))
			{
				Set(x, y, value);
				return true;
			}

			return false;
		}

		public virtual void Set(int index, T value)
		{
			values[index] = value;
		}

		public virtual IGrid2D<T> Clone()
		{
			var grid = new Grid2D<T>(Width, Height);

			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					grid.Set(x, y, Get(x, y));

			return grid;
		}

		public void Clear()
		{
			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					Set(x, y, default(T));
		}
	}
}
