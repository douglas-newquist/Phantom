using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public struct ShipPart
	{
		public SlotState state;

		public ShipPartSO part;

		[Range(-180, 180)]
		public float rotation;

		public bool Occupied => state != SlotState.Free || part != null;

		public ShipPart(SlotState state) : this(null, state, 0) { }

		public ShipPart(ShipPartSO part, float rotation) : this(part, SlotState.Used, rotation) { }

		public ShipPart(ShipPartSO part, SlotState state, float rotation)
		{
			this.state = state;
			this.part = part;
			this.rotation = rotation;
		}

		public GameObject Place(GameObject ship, ShipDesign design, int x, int y)
		{
			if (part == null)
				return null;

			return part.Place(ship, design, x, y);
		}
	}
}
