using System.Collections.Generic;
using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Manages spawning templates for a single object
	/// </summary>
	public class Pool
	{
		private IPoolSpawnCreator spawner;

		private Transform container;

		public Transform Container => container;

		public string Name => container.name;

		private int availableCount = 0;

		private Stack<GameObject> available = new Stack<GameObject>();

		/// <summary>
		///
		/// </summary>
		/// <param name="master">Object to use as a template</param>
		/// <param name="container">Where to store objects</param>
		public Pool(IPoolSpawnCreator spawner, Transform container)
		{
			this.spawner = spawner;
			this.container = container;
		}

		/// <summary>
		/// Creates a new object
		/// </summary>
		public GameObject Create()
		{
			var spawn = spawner.Create();

			if (spawn.TryGetComponent<PoolLink>(out var link) == false)
				link = spawn.AddComponent<PoolLink>();

			link.pool = Name;

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
				spawn = Create();

			spawn.SetActive(true);
			return spawn;
		}
	}
}
