using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class ObjectPool : MonoSingleton<ObjectPool>
	{
		private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

		protected override void OnFirstRun()
		{

		}

		public static Pool GetPool(string id)
		{
			if (Instance.pools.TryGetValue(id, out var tmp))
				return tmp;

			Debug.LogWarning("No pool with the name '" + id + "' exists");
			return null;
		}

		public static bool ContainsPool(string id)
		{
			return Instance.pools.ContainsKey(id);
		}

		/// <summary>
		/// Registers a new object to the pool, uses the object name as the id
		/// </summary>
		/// <param name="obj">Object to register</param>
		public static void Register(GameObject obj)
		{
			Register(obj.name, obj);
		}

		/// <summary>
		/// Registers a new object to the pool
		/// </summary>
		/// <param name="id">Name to register the object by</param>
		/// <param name="obj">Object to register</param>
		public static void Register(string id, GameObject obj)
		{
			if (ContainsPool(id))
			{
				Debug.LogWarning("Object pool with the name '" + id + "' already exists");
				return;
			}

			var container = new GameObject(id).transform;
			container.SetParent(Instance.transform);

			Instance.pools[id] = new Pool(obj, container);
		}

		public static GameObject Spawn(string id, params ISpawner[] spawners)
		{
			return Spawn(id, null, spawners);
		}

		public static GameObject Spawn(string id, Transform parent, params ISpawner[] spawners)
		{
			var pool = GetPool(id);
			if (pool == null) return null;

			var spawn = pool.Spawn();
			spawn.transform.SetParent(parent);

			foreach (var spawner in spawners)
				if (spawner != null)
					spawner.Spawn(spawn);

			spawn.BroadcastMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
			return spawn;
		}

		/// <summary>
		/// Despawns the given object
		/// </summary>
		/// <param name="obj">Object to despawn</param>
		public static void Despawn(GameObject obj)
		{
			if (obj == null) return;

			var link = obj.GetComponent<PoolLink>();

			if (link != null)
				link.Despawn();
			else
				Destroy(obj);
		}
	}
}
