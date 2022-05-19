using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Stats/Resource")]
	public class ResourceStatSO : StatSO
	{
		public float startingPercentage = 1;

		public ResourceStat.ChangedMode maxChangedMode = ResourceStat.ChangedMode.KeepPercentage;

		public override Stat Create()
		{
			return new ResourceStat(this, baseValue, startingPercentage);
		}
	}
}
