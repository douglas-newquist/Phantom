using UnityEngine;

namespace Phantom.StatSystem
{
	[CreateAssetMenu(menuName = StatType.CreateMenu + "Resource")]
	public class ResourceStatType : StatType
	{
		[SerializeField]
		[Range(0f, 1f)]
		private float startingPercentage = 1;

		public float StartingPercentage => startingPercentage;

		[SerializeField]
		private ResourceStat.Changed maxChangedMode = ResourceStat.Changed.KeepPercentage;

		public ResourceStat.Changed MaxChangedMode => maxChangedMode;

		public override IStat Create()
		{
			var resource = new ResourceStat(this, DefaultValue, StartingPercentage);
			resource.MaxChangedMode = MaxChangedMode;
			return resource;
		}
	}
}
