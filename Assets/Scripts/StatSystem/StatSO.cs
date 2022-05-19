using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Stats/Stat")]
	public class StatSO : ScriptableObject
	{
		public float baseValue;

		public virtual Stat Create()
		{
			return new Stat(this, baseValue);
		}
	}
}
