namespace Phantom
{
	public interface IHeap<TValue, TPriority>
	{
		bool Empty { get; }

		TValue Extract();

		void Insert(TValue element, TPriority priority);

		TValue Peek();

		bool TryExtract(out TValue element);
	}
}
