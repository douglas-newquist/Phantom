using System;

namespace Phantom
{
	public interface IHeap<T> where T : IComparable<T>
	{
		int Count { get; }
		bool Empty { get; }

		void Clear();

		/// <summary>
		/// Pops the minimum value off this heap
		/// </summary>
		T Extract();

		/// <summary>
		/// Inserts a new element into this heap
		/// </summary>
		/// <param name="value">Value to insert</param>
		void Insert(T value);

		/// <summary>
		/// Gets the current minimum value without removing it
		/// </summary>
		T Peek();
		bool TryExtract(out T element);

		/// <summary>
		/// Gets the current minimum value without removing it
		/// </summary>
		/// <param name="value">The minimum value</param>
		/// <returns>True if the value is valid</returns>
		bool TryPeek(out T value);
	}
}
