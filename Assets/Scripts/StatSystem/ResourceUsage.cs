namespace Phantom.StatSystem
{
	[System.Serializable]
	public struct ResourceUsage
	{
		public ResourceStatType resource;

		public float amount;

		public bool continuous;
	}
}
