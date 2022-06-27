using UnityEngine;

namespace Phantom.ObjectPooling
{
	/// <summary>
	/// Creates/Copies a GameObject to add to a Pool
	/// </summary>
	public interface IPoolSpawnCreator
	{
		/// <summary>
		/// Generates a new GameObject
		/// </summary>
		GameObject Create();
	}
}
