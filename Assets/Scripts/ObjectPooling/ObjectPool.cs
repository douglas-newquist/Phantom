using System.Collections.Generic;
using UnityEngine;

namespace Phantom.ObjectPooling
{
	public class ObjectPool : MonoSingleton<ObjectPool>
	{
		private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

		public static Pool GetPool(string id)
		{
			if (id == null)
				throw new System.ArgumentNullException("id");

			if (Instance.pools.TryGetValue(id, out var tmp))
				return tmp;

			Debug.LogWarning("No pool with the name '" + id + "' exists");
			return null;
		}

		public static bool ContainsPool(string id)
		{
			if (id == null)
				throw new System.ArgumentNullException("id");
			return Instance.pools.ContainsKey(id);
		}

		/// <summary>
		/// Gets the auto generated name for a prefab
		/// </summary>
		public static string GetPrefabID(GameObject prefab)
		{
			if (prefab == null)
				throw new System.ArgumentNullException("prefab");
			return prefab.name + prefab.GetInstanceID();
		}

		/// <summary>
		/// Registers a new object pool
		/// </summary>
		/// <param name="id">ID to give this object</param>
		/// <param name="spawnFactory">Factory to create new copies</param>
		public static void Register(string id, ISpawnFactory spawnFactory)
		{
			if (ContainsPool(id))
			{
				Debug.LogWarning("Object pool with the name '" + id + "' already exists");
				return;
			}

			var container = new GameObject(id).transform;
			container.SetParent(Instance.transform);

			Instance.pools[id] = new Pool(spawnFactory, container);
		}

		/// <summary>
		/// Registers a new object to the pool, uses the object name as the id
		/// </summary>
		/// <param name="obj">Object to register</param>
		public static void Register(GameObject obj)
		{
			Register(GetPrefabID(obj), obj);
		}

		/// <summary>
		/// Registers a new object to the pool
		/// </summary>
		/// <param name="id">Name to register the object by</param>
		/// <param name="obj">Object to register</param>
		public static void Register(string id, GameObject obj)
		{
			Register(id, new InstantiatePoolSpawner(obj));
		}

		public static GameObject Spawn(GameObject prefab, params ISpawner[] spawners)
		{
			var id = GetPrefabID(prefab);
			if (!ContainsPool(id))
				Register(id, prefab);

			return Spawn(id, spawners);
		}

		public static GameObject Spawn(string id, params ISpawner[] spawners)
		{
			var pool = GetPool(id);
			if (pool == null) return null;

			var spawn = pool.Spawn();

			if (spawners != null)
			{
				foreach (var spawner in spawners)
				{
					if (spawner != null && spawner.Spawn(spawn) == false)
					{
						pool.Return(spawn);
						return null;
					}
				}
			}

			spawn.BroadcastMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

			return spawn;
		}

		/// <summary>
		/// Despawns the given object
		/// </summary>
		/// <param name="obj">Object to despawn</param>
		public static void Despawn(GameObject obj, params ISpawner[] onDespawn)
		{
			if (obj == null) return;

			var link = obj.GetComponent<PoolLink>();

			if (onDespawn != null)
				foreach (var despawn in onDespawn)
					if (despawn != null)
						despawn.Spawn(obj);

			if (link == null || link.pool == null)
				Destroy(obj);
			else if (Instance.pools.TryGetValue(link.pool, out var pool))
				pool.Return(obj);
			else
				Destroy(obj);
		}
	}
}
