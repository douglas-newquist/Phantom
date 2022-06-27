using UnityEngine;

namespace Phantom.StatSystem
{
	[System.Serializable]
	public struct DamageMultiplier
	{
		public ResourceStatType resource;

		[Range(-2f, 2f)]
		public float multiplier;

		public DamageMultiplier(ResourceStatType resource, float multiplier = 1)
		{
			this.resource = resource;
			this.multiplier = multiplier;
		}

		public float Scale(float amount) => amount * multiplier;

		public float Descale(float amount) => amount / multiplier;
	}
}
