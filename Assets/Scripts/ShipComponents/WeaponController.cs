using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Manages and issues commands to multiple groups of turrets
	/// </summary>
	public class WeaponController : MonoBehaviour, IWeaponSystem
	{
		private List<WeaponGroup> groups = new List<WeaponGroup>();

		public IWeaponGroup GetWeaponGroup(int group)
		{
			if (group < 0)
				throw new System.IndexOutOfRangeException("Weapon group must be non-negative.");

			while (groups.Count <= group)
				groups.Add(new WeaponGroup());

			return groups[group];
		}

		public float Aim(Vector2 vector, Reference mode)
		{
			float delta = 0;

			foreach (var group in groups)
				delta += group.Aim(vector, mode);

			return delta;
		}

		public float Aim(Rigidbody2D target)
		{
			float delta = 0;

			foreach (var group in groups)
				delta += group.Aim(target);

			return delta;
		}

		public IEnumerable<GameObject> Fire()
		{
			var projectiles = new List<GameObject>();

			foreach (var group in groups)
				projectiles.AddRange(group.Fire());

			return projectiles;
		}

		public IEnumerable<GameObject> Fire(Vector2 vector, Reference mode)
		{
			var projectiles = new List<GameObject>();

			foreach (var group in groups)
				projectiles.AddRange(group.Fire(vector, mode));

			return projectiles;
		}

		public IEnumerable<GameObject> Fire(Rigidbody2D target)
		{
			var projectiles = new List<GameObject>();

			foreach (var group in groups)
				projectiles.AddRange(group.Fire(target));

			return projectiles;
		}

		public float Aim(int group, Vector2 vector, Reference mode)
		{
			throw new System.NotImplementedException();
		}

		public float Aim(int group, Rigidbody2D target)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<GameObject> Fire(int group, Vector2 vector, Reference mode)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<GameObject> Fire(int group, Rigidbody2D target)
		{
			throw new System.NotImplementedException();
		}

		public void Reset()
		{
			for (int group = 0; group < groups.Count; group++)
				Reset(group);
		}

		public void Reset(int group)
		{
			throw new System.NotImplementedException();
		}

		public void Add(int group, IWeapon weapon)
		{
			GetWeaponGroup(group).Add(weapon);
		}
	}
}
