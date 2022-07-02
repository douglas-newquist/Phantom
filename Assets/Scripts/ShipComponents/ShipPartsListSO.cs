using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "List")]
	public class ShipPartsListSO : ScriptableObject
	{
		public PartTile[] parts;

		public List<PartTile> GetPlaceableParts(ShipDesign design, int x, int y)
		{
			var placable = new List<PartTile>();


			return placable;
		}
	}
}
