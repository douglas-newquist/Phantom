using UnityEngine;

namespace Phantom
{
	public abstract class ShipComponent : MonoBehaviour
	{
		public StatSheet statSheet => GetComponentInParent<StatSheet>();
	}
}
