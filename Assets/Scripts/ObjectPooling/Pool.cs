using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// Manages spawning templates for a single object
	/// </summary>
	public class Pool
	{
		private GameObject master;

		private Transform container;

		private int availableCount = 0;

		private Stack<GameObject> available = new Stack<GameObject>();

		/// <summary>
		///
		/// </summary>
		/// <param name="master">Object to use as a template</param>
		/// <param name="container">Where to store objects</param>
		public Pool(GameObject master, Transform container)
		{
			// Check if master is a prefab
			if (master.scene.rootCount == 0)
				master = GameObject.Instantiate(master);

			this.master = master;
			this.container = container;

			var link = master.AddComponent<PoolLink>();
			link.pool = container.name;

			master.SetActive(false);
			master.transform.SetParent(container);
		}

		/// <summary>
		/// Creates a new object
		/// </summary>
		public GameObject CreateSpawn()
		{
			var spawn = GameObject.Instantiate(master);
			spawn.transform.SetParent(container);
			return spawn;
		}

		/// <summary>
		/// Returns the given object to this pool
		/// </summary>
		/// <param name="spawn">The object to return</param>
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
				spawn = CreateSpawn();

			spawn.SetActive(true);
			return spawn;
		}
	}
}
