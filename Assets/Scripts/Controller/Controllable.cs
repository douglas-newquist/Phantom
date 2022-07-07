using UnityEngine;

namespace Phantom
{
	[DisallowMultipleComponent]
	public sealed class Controllable : MonoBehaviour
	{
		private Coroutine coroutine;

		[SerializeField]
		private Controller controller;

		public Controller Controller
		{
			get => controller;
			set
			{
				StopController();
				controller = value;
				if (controller != null)
					StartController();
			}
		}

		private void OnSpawn()
		{
			if (Controller != null)
				StartController();
		}

		private void StartController()
		{
			if (coroutine != null)
				StopController();
			coroutine = StartCoroutine(Controller.Control(gameObject));
		}

		private void StopController()
		{
			if (coroutine != null)
				StopCoroutine(coroutine);
			coroutine = null;
		}
	}
}
