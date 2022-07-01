using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IWeaponSystem : IWeapon
	{
		IWeaponGroup GetWeaponGroup(int group);

		void Add(int group, IWeapon weapon);

		float Aim(int group, Vector2 vector, Reference mode);

		float Aim(int group, Rigidbody2D target);

		IEnumerable<GameObject> Fire(int group, Vector2 vector, Reference mode);

		IEnumerable<GameObject> Fire(int group, Rigidbody2D target);
	}
}
