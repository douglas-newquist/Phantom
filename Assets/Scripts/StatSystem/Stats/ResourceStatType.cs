using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Stats + "Resource")]
	public class ResourceStatType : StatType
	{
		[Range(0f, 1f)]
		public float startingPercentage = 1;

		public ResourceStat.Changed maxChangedMode = ResourceStat.Changed.KeepPercentage;

		public override IStat Create()
		{
			var resource = new ResourceStat(this, defaultValue, startingPercentage);
			resource.MaxChangedMode = maxChangedMode;
			return resource;
		}
	}
}
