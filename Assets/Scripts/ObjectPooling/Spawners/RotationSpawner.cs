using UnityEngine;

namespace Phantom.ObjectPooling
{
	public struct RotationSpawner : ISpawner
	{
		public FloatRange x, y, z;

		public RotationSpawner(FloatRange x, FloatRange y, FloatRange z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public RotationSpawner(FloatRange z) : this()
		{
			this.z = z;
		}

		public bool Spawn(GameObject obj)
		{
			obj.transform.rotation = Quaternion.Euler(x.Random, y.Random, z.Random);
			return true;
		}
	}
}
