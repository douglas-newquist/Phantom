namespace Game
{
	[System.Serializable]
	public class Slot<T>
	{
		public SlotState state;

		public T item;
	}
}
