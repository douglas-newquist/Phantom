using System.Collections.Generic;

namespace Phantom
{
	[System.Serializable]
	public class WorldStates
	{
		private Dictionary<string, WorldState> states = new Dictionary<string, WorldState>();

		public bool HasState(string key)
		{
			return states.ContainsKey(key);
		}

		public WorldState GetState(string key)
		{
			return states[key];
		}

		public bool TryGetState(string key, out WorldState state)
		{
			return states.TryGetValue(key, out state);
		}

		public void SetState(WorldState state)
		{
			states[state.Key] = state;
		}
	}
}
