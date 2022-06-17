using System.Collections;
using UnityEngine;

namespace Phantom
{
	public abstract class Controller<T> : ScriptableObject
	{
		public void Control(MonoBehaviour monoBehaviour, T controllable)
		{
			if (monoBehaviour == null)
				throw new System.ArgumentNullException("Given MonoBehaviour is null.");

			monoBehaviour.StartCoroutine(Control(controllable));
		}

		public abstract IEnumerator Control(T controllable);
	}
}
