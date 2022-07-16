using System.Collections.Generic;

namespace Phantom
{
	[System.Serializable]
	public class WorldStates
	{
		private Dictionary<string, WorldState> states = new Dictionary<string, WorldState>();

		public WorldStates() { }

		public WorldStates(WorldStates worldStates)
		{
			foreach (var state in worldStates.states)
				SetState(state.Value);
		}

		public override string ToString()
		{
			string s = "World states with " + states.Count + " states";

			foreach (var state in states.Values)
				s += "\n" + state;

			return s;
		}

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

		public void SetStates(IEnumerable<WorldState> states)
		{
			foreach (var state in states)
				SetState(state);
		}
	}
}
