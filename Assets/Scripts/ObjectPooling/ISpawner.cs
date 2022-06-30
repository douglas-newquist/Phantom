using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Performs an action on newly spawned objects from the ObjectPool
	/// </summary>
	public interface ISpawner
	{
		/// <summary>
		/// Applies this spawner
		/// </summary>
		/// <param name="obj">Object being spawned</param>
		/// <returns>True if this spawner was successful</returns>
		bool Spawn(GameObject obj);
	}
}
