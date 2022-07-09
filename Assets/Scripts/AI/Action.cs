using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public abstract class Action : MonoBehaviour, IAction
	{
		[SerializeField]
		private float cost;

		public float Cost => cost;

		[SerializeField]
		private List<WorldSensor> worldSensors = new List<WorldSensor>();

		[SerializeField]
		private List<WorldStateCondition> conditions = new List<WorldStateCondition>();

		[SerializeField]
		private List<WorldState> effects = new List<WorldState>();

		public virtual bool RequiresInRange => false;

		private bool inRange = false;

		public virtual bool InRange
		{
			get => inRange || !RequiresInRange;
			set => inRange = value;
		}

		protected Coroutine coroutine;

		public virtual bool IsRunning { get; protected set; }

		public virtual bool Completed => !IsRunning;

		public virtual bool Perform()
		{
			if (!IsRunning && PreAction())
			{
				coroutine = StartCoroutine(DoAction());
				IsRunning = true;
				return true;
			}

			return false;
		}

		protected virtual bool PreAction() => true;

		protected abstract IEnumerator DoAction();

		public virtual void StopAction()
		{
			if (IsRunning)
			{
				StopCoroutine(coroutine);
				IsRunning = false;
			}
		}

		public virtual void Reset()
		{
			StopAction();
			inRange = false;
		}

		public virtual bool PossibleGiven(WorldStates worldStates)
		{
			foreach (var condition in conditions)
				if (!condition.Satisfied(worldStates))
					return false;

			return true;
		}
	}
}
