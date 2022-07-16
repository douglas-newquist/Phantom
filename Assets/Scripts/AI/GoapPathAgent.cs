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

		/// <summary>
		/// Checks if after performing the given action the agent will have completed all goals
		/// </summary>
		/// <param name="action">Action to perform</param>
		/// <param name="goals"></param>
		/// <returns></returns>
		public bool GoalReached(IAction action, WorldStateCondition[] goals)
		{
			var states = new WorldStates(GetWorldStates(action));
			states.SetStates(action.Effects);
			Debug.Log(states);

			foreach (var goal in goals)
				if (!goal.Satisfied(states))
					return false;

			return true;
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

		public void OnFinishedPathFinding(Path<IAction> path)
		{
			path.Reverse();
		}

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
