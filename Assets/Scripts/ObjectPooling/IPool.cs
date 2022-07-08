using UnityEngine;

namespace Phantom.ObjectPooling
{
	public interface IPool
	{
		/// <summary>
		/// Creates or reuses an object to be used
		/// </summary>
		GameObject Spawn();

		/// <summary>
		/// Returns the given object to this pool
		/// </summary>
		/// <param name="spawn">The object to return</param>
		void Return(GameObject spawn);

		void AddSpawner(ISpawner spawner);

		bool RemoveSpawner(ISpawner spawner);

		/// <summary>
		/// Destroys all inactive copies
		/// </summary>
		void DestroyUnused();
	}
}
