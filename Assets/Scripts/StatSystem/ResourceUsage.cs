namespace Phantom.StatSystem
{
	[System.Serializable]
	public struct ResourceUsage
	{
		public ResourceStatType type;

		public float amount;

		public bool continuous;
	}
}
