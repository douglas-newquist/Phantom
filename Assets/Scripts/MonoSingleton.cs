using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Forces this MonoBehavior to only exist once in the scene
	/// </summary>
	/// <typeparam name="T">Type of this type</typeparam>
	public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
	{
		private static T instance = null;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();

					if (instance == null)
					{
						var obj = new GameObject(typeof(T).Name);
						instance = obj.AddComponent<T>();
						obj.transform.SetParent(GameManager.Instance.transform);
					}

					instance.OnFirstRun();
				}

				return instance;
			}
		}

		protected virtual void Awake()
		{
			if (Instance != this)
			{
				Debug.LogWarning("Multiple instances of " + typeof(T).Name);
				Destroy(this);
			}
		}

		protected abstract void OnFirstRun();
	}
}
