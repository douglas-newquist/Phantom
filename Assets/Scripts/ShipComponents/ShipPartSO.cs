using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Part")]
	public class ShipPartSO : ScriptableObject
	{
		public GameObject prefab;

		[Range(1, 16)]
		public int width = 1, height = 1;

		public TileShape placement = TileShape.Full;

		public List<StatPair> baseStats;

		public virtual bool CanPlace(ShipDesign shipDesign, int x, int y)
		{
			for (int xi = 0; xi < width; xi++)
				for (int yi = 0; yi < height; yi++)
				{
					if (!shipDesign.parts.InBounds(x + xi, y + yi))
						return false;
					if (shipDesign.parts.Get(x + xi, y + yi).Occupied)
						return false;
				}

			var tileShape = shipDesign.tiles.Get(x, y).Shape();
			return tileShape == placement;
		}

		public void Place(ShipDesign shipDesign, int x, int y)
		{
			shipDesign.parts.Set(x, y, new ShipPart(this, SlotState.Used, 0));

			for (int xi = 0; xi < width; xi++)
				for (int yi = 0; yi < height; yi++)
					if (xi != 0 || yi != 0)
						shipDesign.parts.Set(x + xi, y + yi, new ShipPart(SlotState.Reserved));
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="design"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>Reference to the part added if any</returns>
		public GameObject Place(GameObject ship, ShipDesign design, int x, int y)
		{
			GameObject part = null;

			if (prefab != null)
			{
				part = Instantiate(prefab);
			}

			return part;
		}
	}
}
