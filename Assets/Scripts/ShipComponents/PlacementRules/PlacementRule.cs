using UnityEngine;

namespace Phantom
{
	public abstract class PlacementRule : ScriptableObject
	{
		public abstract bool CanPlace(ShipPartSO part, ShipDesign design, int x, int y);
	}
}
