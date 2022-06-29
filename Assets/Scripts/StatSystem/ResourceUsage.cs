using UnityEngine;

namespace Phantom.StatSystem
{
	[System.Serializable]
	public struct ResourceUsage
	{
		[SerializeField]
		private ResourceStatType type;

		[SerializeField]
		private float amount;

		[SerializeField]
		private bool continuous;
	}
}
