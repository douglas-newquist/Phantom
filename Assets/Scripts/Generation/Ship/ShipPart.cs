using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public struct ShipPart
	{
		public Reservation state;

		public ShipPartSO part;

		[Range(-180, 180)]
		public float rotation;

		public bool Occupied => state != Reservation.Free || part != null;

		public ShipPart(Reservation state) : this(null, state, 0) { }

		public ShipPart(ShipPartSO part, float rotation) : this(part, Reservation.Used, rotation) { }

		public ShipPart(ShipPartSO part, Reservation state, float rotation)
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
