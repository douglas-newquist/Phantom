using UnityEngine;

namespace Game
{
	[DisallowMultipleComponent]
	public class Entity : MonoBehaviour
	{
		public StatSheet Stats => GetComponent<StatSheet>();

		public ResourceStatSO healthStat;

		public ResourceStat Health => Stats.GetStat<ResourceStat>(healthStat);
	}
}
