using UnityEngine;
using System.Collections;

namespace Game
{
	[DisallowMultipleComponent]
	public class Controllable : MonoBehaviour
	{
		public Controller controller;

		[System.NonSerialized]
		IEnumerator running = null;

		public void Start()
		{
			if (controller != null)
			{
				running = controller.Control(this);
				StartCoroutine(running);
			}
			else
				Debug.LogWarning("No controller provided!");
		}

		public void Stop()
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
