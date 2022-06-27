using UnityEngine;

namespace Phantom.ObjectPooling
{
	public struct PositionSpawner : ISpawner
	{
		public FloatRange x, y, z;

		public PositionSpawner(Vector3 location)
		{
			x = location.x;
			y = location.y;
			z = location.z;
		}

		public PositionSpawner(FloatRange x, FloatRange y, FloatRange z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public PositionSpawner(FloatRange x, FloatRange y) : this(x, y, 0) { }

		public PositionSpawner(Rect area)
		{
			x = new FloatRange(area.xMin, area.xMax);
			y = new FloatRange(area.yMin, area.yMax);
			z = 0;
		}

		public void Spawn(GameObject obj)
		{
			obj.transform.position = new Vector3(x.Random, y.Random, z.Random);
		}
	}
}
