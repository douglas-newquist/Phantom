using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	public interface IFire
	{
		IEnumerable<GameObject> Fire();

		IEnumerable<GameObject> Fire(Vector2 vector, Reference mode);

		IEnumerable<GameObject> Fire(Rigidbody2D target);
	}
}
