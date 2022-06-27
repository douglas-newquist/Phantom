using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Stats + "Resource")]
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
