using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class ShipPart
	{
		public SlotState state;

		public ShipPartSO part;

		[Range(-180, 180)]
		public float rotation;
	}
}
