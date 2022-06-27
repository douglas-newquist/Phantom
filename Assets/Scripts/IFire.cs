using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IFire
	{
		IEnumerable<Projectile> Fire();

		IEnumerable<Projectile> Fire(Vector2 vector, Reference mode);

		IEnumerable<Projectile> Fire(Rigidbody2D target);
	}
}
