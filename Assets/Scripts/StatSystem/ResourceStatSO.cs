using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Stats/Resource")]
	public class ResourceStatSO : StatSO
	{
		[Range(0f, 1f)]
		public float startingPercentage = 1;

		public ResourceStat.ChangedMode maxChangedMode = ResourceStat.ChangedMode.KeepPercentage;

		public override Stat Create()
		{
			var resource = new ResourceStat(this, defaultValue, startingPercentage);
			resource.MaxChangedMode = maxChangedMode;
			return resource;
		}
	}
}
