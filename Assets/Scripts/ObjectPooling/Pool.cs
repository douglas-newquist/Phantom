using System.Collections.Generic;
using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Manages spawning templates for a single object
	/// </summary>
	public sealed class Pool : IPool
	{
		private ISpawnFactory factory;

		private Transform container;

		public Transform Container => container;

		public string Name => container.name;

		private Stack<GameObject> available = new Stack<GameObject>();

		private List<ISpawner> spawners = new List<ISpawner>();

		/// <summary>
		///
		/// </summary>
		/// <param name="master">Object to use as a template</param>
		/// <param name="container">Where to store objects</param>
		/// <param name="spawners">Spawners to call when spawning</param>
		public Pool(ISpawnFactory factory, Transform container, params ISpawner[] spawners)
		{
			this.container = container;
			this.factory = factory;
			factory.AddSpawner(new PoolLinkSpawner(Name));

			this.spawners.Add(new SetParentSpawner(Container));
			if (spawners != null)
				this.spawners.AddRange(spawners);
		}
		public void Return(GameObject spawn)
		{
			spawn.SetActive(false);
			spawn.transform.SetParent(container);
			available.Push(spawn);
		}

		/// <summary>
		/// Creates or reuses an object to be used
		/// </summary>
		public GameObject Spawn()
		{
			GameObject spawn;

			if (available.Count > 0)
				spawn = available.Pop();
			else
				spawn = factory.Create();

			spawn.SetActive(true);

			foreach (var spawner in spawners)
			{
				if (spawner != null && spawner.Spawn(spawn) == false)
				{
					Return(spawn);
					return null;
				}
			}

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
