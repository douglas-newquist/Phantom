using UnityEngine;

#if UNITY_EDITOR
namespace Game
{
	public class MinMax : PropertyAttribute
	{
		public readonly float min, max;

		public MinMax(float min, float max)
		{
			this.min = min;
			this.max = max;
		}
	}
}
#endif