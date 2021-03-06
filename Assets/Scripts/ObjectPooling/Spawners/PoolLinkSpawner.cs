using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Attaches the pool link to the game object
	/// </summary>
	public struct PoolLinkSpawner : ISpawner
	{
		private string name;

		public PoolLinkSpawner(string name)
		{
			this.name = name;
		}

		public bool Spawn(GameObject obj)
		{
			if (obj.TryGetComponent<PoolLink>(out var link))
			{
				link.pool = name;
			}
			else
			{
				link = obj.AddComponent<PoolLink>();
				link.pool = name;
			}

			return true;
		}
	}
}
