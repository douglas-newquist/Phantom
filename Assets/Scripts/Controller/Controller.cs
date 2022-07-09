using System.Collections;
using UnityEngine;

namespace Phantom
{
	public abstract class Controller : ScriptableObject
	{
		public const string CreateMenu = "Game/Controller/";

		public void Control(MonoBehaviour monoBehaviour, GameObject gameObject)
		{
			if (monoBehaviour == null)
				throw new System.ArgumentNullException("Given MonoBehaviour is null.");

			monoBehaviour.StartCoroutine(Control(gameObject));
		}

		public abstract IEnumerator Control(GameObject gameObject);
	}
}
