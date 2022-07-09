using UnityEngine;

namespace Phantom
{
	public static class EntityExtensions
	{
		/// <summary>
		/// Checks if this entity has line of sight to the given entity
		/// </summary>
		/// <param name="self">Source entity</param>
		/// <param name="other">Target entity</param>
		/// <param name="mask">Physics layers for raycast</param>
		public static bool LineOfSight(this IEntity self, IEntity other, LayerMask mask)
		{
			if (self == null) throw new System.ArgumentNullException("self");
			if (other == null) throw new System.ArgumentNullException("other");

			Vector2 start = self.transform.position;
			Vector2 end = other.transform.position;
			Vector2 delta = end - start;

			var hit = Physics2D.Raycast(start, delta.normalized, delta.magnitude);
			return hit.transform == null || hit.transform == other.transform;
		}

		/// <summary>
		/// Checks if this entity has a given attitude towards another
		/// </summary>
		/// <param name="self">Source entity</param>
		/// <param name="attitudes">Attitude(s) to check for</param>
		/// <param name="other">Target entity</param>
		public static bool HasAttitudeTowards(this IEntity self, Attitude attitudes, IEntity other)
		{
			if (self == null) throw new System.ArgumentNullException("self");
			if (other == null) throw new System.ArgumentNullException("other");

			return self.GetAttitudeTowards(other).Matches(attitudes);
		}
	}
}
