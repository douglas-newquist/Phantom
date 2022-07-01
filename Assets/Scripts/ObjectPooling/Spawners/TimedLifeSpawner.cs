using System.Collections;
using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Automatically despawns a given object after some amount of time
	/// </summary>
	public struct TimedLifeSpawner : ISpawner
	{
		FloatRange lifeSpan;
		ISpawner[] onDespawn;

		public TimedLifeSpawner(FloatRange lifeSpan, params ISpawner[] onDespawn)
		{
			this.onDespawn = onDespawn;
			this.lifeSpan = lifeSpan;
		}

		public bool Spawn(GameObject obj)
		{
			if (obj.TryGetComponent<PoolLink>(out var link))
			{
				link.StartCoroutine(DelayedDespawn(obj));
				return true;
			}

			if (obj.TryGetComponent<MonoBehaviour>(out var behaviour))
			{
				behaviour.StartCoroutine(DelayedDespawn(obj));
				return true;
			}

			return false;
		}

		IEnumerator DelayedDespawn(GameObject obj)
		{
			yield return new WaitForSeconds(lifeSpan.Random);
			ObjectPool.Despawn(obj, onDespawn);
		}
	}
}
