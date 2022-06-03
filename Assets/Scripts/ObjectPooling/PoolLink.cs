using UnityEngine;

namespace Game
{
	/// <summary>
	/// Specifies what object pool this object belongs to
	/// </summary>
	public class PoolLink : MonoBehaviour
	{
		public string pool;

		/// <summary>
		/// Despawns this object
		/// </summary>
		public void Despawn()
		{
			if (pool != null)
				ObjectPool.Despawn(gameObject);
			else
				Destroy(gameObject);
		}
	}
}
