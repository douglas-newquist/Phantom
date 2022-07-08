namespace Phantom
{
	[System.Serializable]
	public class WorldStates
	{
		public bool HasState(string key)
		{
			throw new System.NotImplementedException();
		}

		public WorldState GetState(string key)
		{
			throw new System.NotImplementedException();
		}

		public bool TryGetState(string key, out WorldState state)
		{
			throw new System.NotImplementedException();
		}

		public void SetState(string key, int value)
		{

		}

		public void SetState(WorldState state)
		{

		}
	}
}
