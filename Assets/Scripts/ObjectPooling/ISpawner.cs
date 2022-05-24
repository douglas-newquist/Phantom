using UnityEngine;

namespace Game
{
	public interface ISpawner
	{
		void Spawn(GameObject obj);
	}
	public struct PositionSpawner : ISpawner
	{
		public FloatRange x, y;

		public void Spawn(GameObject obj)
		{

		}
	}
}
