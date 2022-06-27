using UnityEngine;

namespace Phantom.ObjectPooling
{
	public struct RotationSpawner : ISpawner
	{
		public FloatRange angle;

		public RotationSpawner(FloatRange angle)
		{
			this.angle = angle;
		}

		public void Spawn(GameObject obj)
		{
			obj.transform.rotation = Quaternion.Euler(0, 0, angle.Random);
		}
	}
}
