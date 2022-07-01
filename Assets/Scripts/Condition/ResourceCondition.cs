using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	/// <summary>
	/// Checks if the attached stat sheet's resource is within range
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu.Condition + "Resource Thresh Hold")]
	public class ResourceCondition : Condition
	{
		public ResourceStatType resource;

		[MinMax(0f, 1f)]
		public FloatRange percentage = new FloatRange(0, 0.5f);

		public bool isInsideRange = true;

		public override bool Satisfied(GameObject gameObject)
		{
			var statSheet = gameObject.GetComponent<StatSheet>();
			if (statSheet == null)
				return false;

			ResourceStat r = statSheet.GetStat<ResourceStat>(resource);

			var inside = r.Percentage >= percentage.Min && r.Percentage <= percentage.Max;
			return inside == isInsideRange;
		}
	}
}
