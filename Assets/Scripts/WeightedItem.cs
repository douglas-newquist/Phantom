namespace Game
{
	[System.Serializable]
	public class WeightedItem<T>
	{
		public T value;
		public float weight;

		public WeightedItem(T value, float weight)
		{
			this.value = value;
			this.weight = weight;
		}
	}
}
