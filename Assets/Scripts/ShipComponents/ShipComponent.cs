using UnityEngine;
using Phantom.StatSystem;

namespace Phantom
{
	public abstract class ShipComponent : MonoBehaviour
	{
		public Ship Ship => GetComponentInParent<Ship>();

		public StatSheet StatSheet => GetComponentInParent<StatSheet>();
	}
}
