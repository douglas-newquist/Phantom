using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public struct DamageMultiplier
	{
		public ResourceStatSO resource;

		[Range(-2f, 2f)]
		public float multiplier;

		public DamageMultiplier(ResourceStatSO resource, float multiplier = 1)
		{
			this.resource = resource;
			this.multiplier = multiplier;
		}
	}
}
