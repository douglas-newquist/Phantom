using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Uses GameObject.Instantiate to create copies of a prefab
	/// </summary>
	public class InstantiatePoolSpawner : ISpawnFactory
	{
		public GameObject master;

		public InstantiatePoolSpawner(GameObject master)
		{
			this.master = master;

			// Check if master is not a prefab
			if (master.scene.rootCount != 0)
			{
				master.SetActive(false);
			}
		}

		public GameObject Create()
		{
			var spawn = GameObject.Instantiate(master);
			return spawn;
		}
	}
}
