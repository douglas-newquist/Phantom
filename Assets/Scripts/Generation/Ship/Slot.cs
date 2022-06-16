namespace Phantom
{
	[System.Serializable]
	public class Slot<T>
	{
		public Reservation state;

		public T item;
	}
}
