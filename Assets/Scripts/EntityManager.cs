using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Phantom
{
	public class EntityManager : MonoSingleton<EntityManager>
	{
		private List<IEntity> entities = new List<IEntity>();

		public static IEnumerable<IEntity> Entities => Instance.entities.Where(e => e.gameObject.activeInHierarchy);

		public static void Track(GameObject gameObject)
		{
			if (gameObject != null && gameObject.TryGetComponent(out IEntity entity))
				Track(entity);
		}

		public static void Track(IEntity entity)
		{
			if (entity != null)
			{
				Instance.entities.Add(entity);
				Debug.Log("Tracking " + entity.gameObject.name);
			}
		}

		public static void StopTracking(GameObject gameObject)
		{
			if (gameObject != null && gameObject.TryGetComponent(out IEntity entity))
				StopTracking(entity);
		}

		public static bool StopTracking(IEntity entity)
		{
			return Instance.entities.Remove(entity);
		}

		public static IEnumerable<IEntity> GetTargets(Vector3 point, float range)
		{
			return Entities.Where(e => Vector3.Distance(e.transform.position, point) < range);
		}
	}
}
