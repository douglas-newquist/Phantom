using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class TileObject
	{
		[SerializeField]
		private MapTile tile;

		/// <summary>
		/// The object placed in this tile
		/// </summary>
		public MapTile Tile
		{
			get => tile;
			set
			{
				if (state == Reservation.Locked)
					throw new System.InvalidOperationException("Cannot change the object, this tile object is currently locked");

				Clear();
				if (value == null) return;
				tile = value;
				state = Reservation.Used;
			}
		}

		[SerializeField]
		private Reservation state = Reservation.Free;

		/// <summary>
		/// The current state of this tile object
		/// </summary>
		public Reservation State
		{
			get => state;
			set
			{
				switch (state)
				{
					case Reservation.Used:
					case Reservation.Reserved:
						throw new System.InvalidOperationException("Cannot change the state while it is currently being used");
				}

				switch (value)
				{
					case Reservation.Used:
					case Reservation.Reserved:
						throw new System.InvalidOperationException("Cannot set the state to " + value);
				}

				Clear();
				state = value;
			}
		}

		public bool NotOccupied => State == Reservation.Free && Tile == null;

		public bool Occupied => !NotOccupied;

		[SerializeField]
		private Vector3Int reference;

		/// <summary>
		/// Points to the cell containing the actual object
		/// </summary>
		public Vector3Int Reference
		{
			get => reference;
			set
			{
				if (state == Reservation.Locked)
					throw new System.InvalidOperationException("Cannot change the reference, this tile object is currently locked");

				Clear();
				state = Reservation.Reserved;
				reference = value;
			}
		}

		[SerializeField]
		private int variant;

		public int Variant
		{
			get => variant;
			set => variant = value;
		}

		public int[] special;

		public TileObject() { }

		public TileObject(MapTile obj)
		{
			Tile = obj;
		}

		public TileObject(Reservation state)
		{
			State = state;
		}

		public TileObject(Vector3Int reference)
		{
			Reference = reference;
		}

		public void Clear()
		{
			tile = null;
			state = Reservation.Free;
			reference = Vector3Int.zero;
			variant = 0;
			special = null;
		}
	}
}
