using Phantom.Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Phantom
{
	[DisallowMultipleComponent]
	public sealed class GoapPlanner : MonoBehaviour
	{
		private HashSet<IAction> actions = new HashSet<IAction>();

		private HashSet<WorldSensor> worldSensors = new HashSet<WorldSensor>();

		/// <summary>
		/// Is the GOAP controller currently executing a plan
		/// </summary>
		public bool Executing => coroutine != null;

		public bool HasPlan => plan != null;

		private Coroutine coroutine;

		private Stack<IAction> plan = null;

		[SerializeField]
		private Pathfinder pathfinder;

		private GoapPathAgent agent;

		public void AddAction(IAction action)
		{
			actions.Add(action);

			foreach (var sensor in action.WorldSensors)
				worldSensors.Add(sensor);
		}

		public bool RemoveAction(IAction action)
		{
			if (actions.Remove(action))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Generates a sequence of actions
		/// </summary>
		/// <returns>Returns true if a plan was successfully generated</returns>
		public bool Plan(params WorldStateCondition[] goals)
		{
			var worldStates = new WorldStates();

			foreach (var sensor in worldSensors)
				worldStates.SetState(sensor.GetWorldState(gameObject));

			agent = new GoapPathAgent(worldStates);
			var request = new PathRequest<IEnumerable<IAction>, IAction>()
			{
				Map = actions,
				Agent = agent,
				StartingCells = actions.Where(a => a.PossibleGiven(worldStates)),
				GoalReached = (map, action) => agent.GoalReached(action, goals)
			};

			pathfinder.FindPath(request);
			Debug.Log(request.Path.Length);

			plan = new Stack<IAction>();
			foreach (var action in request.Path)
				plan.Push(action);

			return true;
		}

		/// <summary>
		/// Starts executing the current plan
		/// </summary>
		/// <returns>True if started</returns>
		public bool Execute()
		{
			if (!HasPlan || Executing) return false;

			coroutine = StartCoroutine(DoPlan());
			return true;
		}

		/// <summary>
		/// Stops the execution of the current plan
		/// </summary>
		public void Abort()
		{
			if (Executing)
			{
				StopCoroutine(coroutine);
				PostPlan();
			}
		}

		public void ScanActions()
		{
			foreach (var action in GetComponentsInChildren<IAction>())
				AddAction(action);
		}

		private IEnumerator DoPlan()
		{
			while (plan.TryPop(out IAction action))
			{
				if (!action.InRange)
				{
					IMover mover = GetComponent<IMover>();
					mover.MoveTo(action.StartingLocation);
					yield return new WaitUntil(() => action.InRange);
				}

				if (!action.Perform())
				{
					Abort();
					break;
				}

				yield return new WaitUntil(() => action.Completed);
			}

			PostPlan();
		}

		private void PostPlan()
		{
			coroutine = null;
			plan = null;
		}
	}
}
