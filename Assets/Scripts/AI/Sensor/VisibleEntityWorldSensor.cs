using System.Linq;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu + "Entity Visible")]
	public sealed class VisibleEntityWorldSensor : WorldSensor
	{
		[SerializeField]
		private Attitude attitudes = Attitude.Hostile;

		[SerializeField]
		[Range(0f, Level.TotalSizeLimit)]
		private float range = Level.TileSize * 10;

		[SerializeField]
		private bool lineOfSight = true;

		[SerializeField]
		private LayerMask layerMask;

		public override WorldState GetWorldState(GameObject gameObject)
		{
			if (gameObject.TryGetComponent(out IEntity self))
			{
				var entity = EntityManager.Entities.First(other => Predicate(self, other));
				if (entity != null)
					return new WorldState(Key, true);
			}

			return new WorldState(Key, false);
		}

		private bool Predicate(IEntity self, IEntity other)
		{
			if (Vector2.Distance(self.transform.position, other.transform.position) > range)
				return false;

			if (!self.HasAttitudeTowards(attitudes, other))
				return false;

			if (lineOfSight && !self.LineOfSight(other, layerMask))
				return false;

			return true;
		}
	}
}
