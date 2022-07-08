using System.Collections.Generic;
using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Manages spawning templates for a single object
	/// </summary>
	public sealed class Pool : IPool
	{
		private ISpawnFactory spawner;

		private Transform container;

		public Transform Container => container;

		public string Name => container.name;

		private int availableCount = 0;

		private Stack<GameObject> available = new Stack<GameObject>();

		private List<ISpawner> spawners = new List<ISpawner>();

		/// <summary>
		///
		/// </summary>
		/// <param name="master">Object to use as a template</param>
		/// <param name="container">Where to store objects</param>
		/// <param name="spawners">Spawners to call when spawning</param>
		public Pool(ISpawnFactory spawner, Transform container, params ISpawner[] spawners)
		{
			this.spawner = spawner;
			this.container = container;

			this.spawners.Add(new PoolLinkSpawner(Name));
			if (spawners != null)
				this.spawners.AddRange(spawners);
		}
		public void Return(GameObject spawn)
		{
			spawn.SetActive(false);
			spawn.transform.SetParent(container);
			available.Push(spawn);
			availableCount++;
		}

		/// <summary>
		/// Creates or reuses an object to be used
		/// </summary>
		public GameObject Spawn()
		{
			GameObject spawn;

			if (availableCount > 0)
			{
				availableCount--;
				spawn = available.Pop();
			}
			else
				spawn = spawner.Create();

			spawn.SetActive(true);
			return spawn;
		}

		public void AddSpawner(ISpawner spawner)
		{
			spawners.Add(spawner);
		}

		public bool RemoveSpawner(ISpawner spawner)
		{
			return spawners.Remove(spawner);
		}

		public void DestroyUnused()
		{
			foreach (var obj in available)
				GameObject.Destroy(obj);
			available.Clear();
		}
	}
}
