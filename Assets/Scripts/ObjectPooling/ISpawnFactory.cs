using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Creates/Copies a GameObject to add to a Pool
	/// </summary>
	public interface ISpawnFactory
	{
		/// <summary>
		/// Generates a new GameObject
		/// </summary>
		GameObject Create();

		void AddSpawner(ISpawner spawner);

		bool RemoveSpawner(ISpawner spawner);
	}
}
