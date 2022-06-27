using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public static class TransformExtensions
	{
		public static IEnumerable<T> GetComponentsOnlyInChildren<T>(this Transform transform, bool recursively = true)
		{
			var components = new List<T>();

			foreach (Transform child in transform)
				if (recursively)
					components.AddRange(child.GetComponentsInChildren<T>());
				else
					components.AddRange(child.GetComponents<T>());

			return components;
		}
	}
}
