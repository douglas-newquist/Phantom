using UnityEngine;

namespace Phantom
{
	public abstract class ShipComponent : MonoBehaviour
	{
		public Ship ship => GetComponentInParent<Ship>();

		public StatSheet statSheet => GetComponentInParent<StatSheet>();
	}
}
