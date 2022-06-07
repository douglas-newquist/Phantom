using UnityEngine;
using System.Collections;

namespace Phantom
{
	[DisallowMultipleComponent]
	public class Controllable : MonoBehaviour
	{
		public Controller controller;

		[System.NonSerialized]
		protected IEnumerator running = null;

		public virtual void Start()
		{
			if (controller != null)
			{
				running = controller.Control(this);
				StartCoroutine(running);
			}
			else
				Debug.LogWarning("No controller provided!");
		}

		public virtual void Stop()
		{
			if (running != null)
			{
				StopCoroutine(running);
				running = null;
			}
		}

		public virtual void Move(Vector3 direction)
		{

		}
	}
}
