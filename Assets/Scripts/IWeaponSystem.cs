using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IWeaponSystem : IWeapon
	{
		IWeapon GetWeaponGroup(int group);

		float Aim(int group, Vector2 vector, Reference mode);

		float Aim(int group, Rigidbody2D target);

		IEnumerable<Projectile> Fire(int group, Vector2 vector, Reference mode);

		IEnumerable<Projectile> Fire(int group, Rigidbody2D target);
	}
}
