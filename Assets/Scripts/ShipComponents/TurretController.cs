using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// Manages and issues commands to multiple groups of turrets
	/// </summary>
	public class TurretController : MonoBehaviour
	{
		public List<TurretGroup> groups = new List<TurretGroup>();

		public TurretGroup GetGroup(int group)
		{
			if (group >= 0 && group < groups.Count)
				return groups[group];
			return null;
		}

		public List<Projectile> Aim(Rigidbody2D target, bool fire)
		{
			var projectiles = new List<Projectile>();

			for (int group = 0; group < groups.Count; group++)
			{
				var fired = Aim(target, group, fire);
				if (fired != null)
					projectiles.AddRange(fired);
			}

			return projectiles;
		}

		public List<Projectile> Aim(Rigidbody2D target, int group, bool fire)
		{
			var turrets = GetGroup(group);
			if (turrets == null)
				return null;

			if (fire)
				return turrets.Fire(target);

			turrets.Look(target.position, Reference.Absolute);
			return null;
		}

		public List<Projectile> Aim(Vector3 vector, Reference mode, bool fire)
		{
			var projectiles = new List<Projectile>();

			for (int group = 0; group < groups.Count; group++)
			{
				var fired = Aim(vector, mode, group, fire);
				if (fired != null)
					projectiles.AddRange(fired);
			}

			return projectiles;
		}

		public List<Projectile> Aim(Vector3 vector, Reference mode, int group, bool fire)
		{
			var turrets = GetGroup(group);
			if (turrets == null)
				return null;

			if (fire)
				return turrets.Fire(vector, mode);

			turrets.Look(vector, mode);
			return null;
		}

		public void Reset()
		{
			for (int group = 0; group < groups.Count; group++)
				Reset(group);
		}

		public void Reset(int group)
		{
			var turrets = GetGroup(group);
			if (turrets != null)
				turrets.Reset();
		}
	}
}
