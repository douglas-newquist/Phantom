using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "List")]
	public class ShipPartsListSO : ScriptableObject
	{
		public ShipPartSO[] parts;

		public List<ShipPartSO> GetPlaceableParts(ShipDesign design, int x, int y)
		{
			var placable = new List<ShipPartSO>();

			foreach (var part in parts)
				if (part.CanPlace(design, x, y))
					placable.Add(part);

			return placable;
		}
	}
}
