using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu + "Resource Sensor")]
	public sealed class ResourceWorldSensor : WorldSensor
	{
		[SerializeField]
		private ResourceStatType resourceType;

		public enum Value
		{
			Percentage,
			Current,
			Missing
		}

		[SerializeField]
		private Value valueToRead;

		public override WorldState GetWorldState(GameObject gameObject)
		{
			int value = 0;

			if (gameObject.TryGetComponent<StatSheet>(out var statSheet))
			{
				if (statSheet.TryGetStat(resourceType, out IResourceStat resource))
				{
					switch (valueToRead)
					{
						case Value.Percentage:
							value = Mathf.RoundToInt(resource.Percentage * 100);
							break;

						case Value.Current:
							value = Mathf.RoundToInt(resource.Current);
							break;

						case Value.Missing:
							value = Mathf.RoundToInt(resource.Missing);
							break;
					}
				}
			}

			return new WorldState(Key, value);
		}
	}
}
