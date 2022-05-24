using UnityEngine;

namespace Game
{
	/// <summary>
	/// Specifies what object pool this object belongs to
	/// </summary>
	public class PoolLink : MonoBehaviour
	{
		public Pool Pool { get; set; }

		/// <summary>
		/// Despawns this object
		/// </summary>
		public void Despawn()
		{
			if (Pool != null)
				Pool.Return(gameObject);
			else
				Destroy(gameObject);
		}
	}
}
