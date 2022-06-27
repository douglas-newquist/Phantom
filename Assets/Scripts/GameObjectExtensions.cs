using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public static class GameObjectExtensions
	{
		public static IEnumerable<T> GetComponentsOnlyInChildren<T>(this GameObject gameObject, bool recursively = true)
		{
			return gameObject.transform.GetComponentsOnlyInChildren<T>(recursively);
		}
	}
}
