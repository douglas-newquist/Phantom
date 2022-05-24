namespace Game
{
	[System.Serializable]
	public class ShipPart
	{
		public SlotState state;
		public StatList stats;
	}
	public enum SlotState
	{
		Free,
		Used,
		Reserved,
		Locked
	}
	[System.Serializable]
	public class Slot<T>
	{
		public SlotState state;

		public T item;
	}
}
