using Phantom.Pathfinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Phantom
{
	public sealed class GoapPathAgent : IPathAgent<IEnumerable<IAction>, IAction>
	{
		WorldStates worldStates;

		Dictionary<IAction, WorldStates> cellWorldStates;

		public bool ChecksCompletePath => true;

		public GoapPathAgent(WorldStates worldStates)
		{
			this.worldStates = worldStates;
			cellWorldStates = new Dictionary<IAction, WorldStates>();
		}

		WorldStates GetWorldStates(IAction action)
		{
			if (cellWorldStates.TryGetValue(action, out var states))
				return states;

			return worldStates;
		}

		public IEnumerable<IAction> GetNeighbors(IEnumerable<IAction> map, IAction pos)
		{
			var states = GetWorldStates(pos);
			return map.Where(a => a.PossibleGiven(states));
		}

		public float GetPathCost(IEnumerable<IAction> map, IAction start, IAction end)
		{
			var states = GetWorldStates(start);
			return start.PossibleGiven(states) ? start.Cost : -1;
		}

		public float GetSubPathExtraCost(IEnumerable<IAction> map, IAction a, IAction b, IAction c)
		{
			return 0;
		}

		public void OnBetterPathFound(IEnumerable<IAction> map, IAction original, IAction current, IAction end)
		{
			var states = new WorldStates(GetWorldStates(original));
			cellWorldStates[current] = states;
		}

		public void OnFinishedPathFinding(Path<IAction> path) { }

		public bool GetCompletePathPossible(IEnumerable<IAction> map, IEnumerable<IAction> path)
		{
			var states = new WorldStates(worldStates);

			foreach (var action in path)
			{
				if (!action.PossibleGiven(states))
					return false;

				states.SetStates(action.Effects);
			}

			return true;
		}
	}
}
