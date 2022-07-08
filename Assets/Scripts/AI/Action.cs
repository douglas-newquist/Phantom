using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public abstract class Action : MonoBehaviour, IAction
	{
		[SerializeField]
		private int priority;

		public virtual int Priority => priority;

		public WorldSensor[] worldSensors;

		public WorldStateCondition[] conditions;

		public WorldState[] effects;

		public virtual bool IsPossible
		{
			get
			{

				return true;
			}
		}

		protected Coroutine coroutine;

		public virtual bool IsRunning => coroutine != null;

		public virtual bool Completed => !IsRunning;

		public virtual bool Perform()
		{
			if (IsPossible && !IsRunning && PreAction())
			{
				coroutine = StartCoroutine(DoAction());
				return true;
			}

			return false;
		}

		protected virtual bool PreAction() => true;

		protected abstract IEnumerator DoAction();

		public virtual void StopAction()
		{
			if (IsRunning)
				StopCoroutine(coroutine);
		}
	}
}
