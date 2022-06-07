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
			var tileShape = shipDesign.tiles.Get(x, y).Shape();
			return placement.HasFlag(tileShape);
		}

		public void Place(ShipDesign shipDesign, int x, int y)
		{
			shipDesign.parts.Set(x, y, new ShipPart(this, SlotState.Used, 0));
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
			return null;
		}
	}
}
