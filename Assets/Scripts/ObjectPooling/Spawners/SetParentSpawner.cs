using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Sets the GameObject's parent transform
	/// </summary>
	public struct SetParentSpawner : ISpawner
	{
		public Transform parent;

		public SetParentSpawner(Transform parent)
		{
			this.parent = parent;
		}

		public bool Spawn(GameObject obj)
		{
			obj.transform.SetParent(parent);
			return true;
		}
	}
}
