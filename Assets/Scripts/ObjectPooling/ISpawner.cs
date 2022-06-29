using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Performs an action on newly spawned objects from the ObjectPool
	/// </summary>
	public interface ISpawner
	{
		void Spawn(GameObject obj);
	}
}
