using System.Collections.Generic;
using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Uses GameObject.Instantiate to create copies of a prefab
	/// </summary>
	public sealed class InstantiateSpawnFactory : ISpawnFactory
	{
		private GameObject master;

		private List<ISpawner> spawners = new List<ISpawner>();

		/// <summary>
		///
		/// </summary>
		/// <param name="master">Prefab or GameObject to copy</param>
		/// <param name="spawners">Spawners to run when creating a new copy</param>
		public InstantiateSpawnFactory(GameObject master, params ISpawner[] spawners)
		{
			this.master = master;
			if (spawners != null)
				this.spawners.AddRange(spawners);

			// Check if master is not a prefab
			if (master.scene.rootCount != 0)
			{
				master.SetActive(false);
			}
		}

		public void AddSpawner(ISpawner spawner)
		{
			spawners.Add(spawner);
		}

		public GameObject Create()
		{
			var spawn = GameObject.Instantiate(master);

			foreach (var spawner in spawners)
				if (spawner != null)
					spawner.Spawn(spawn);

			return spawn;
		}

		public bool RemoveSpawner(ISpawner spawner)
		{
			return spawners.Remove(spawner);
		}
	}
}
