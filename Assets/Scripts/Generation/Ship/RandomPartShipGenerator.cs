using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipGenerator + "Random Parts")]
	public class RandomPartShipGenerator : ShipGenerator
	{
		public ShipPartsListSO parts;

		[MinMax(0, 1)]
		public FloatRange density = 0.5f;

		public override ShipDesign ApplyOnce(ShipDesign design, RectInt area)
		{
			design = new ShipDesign(design);
			float thresh = density.Random;

			for (int x = area.xMin; x < area.xMax; x++)
			{
				for (int y = area.yMin; y < area.yMax; y++)
				{
					var placable = parts.GetPlaceableParts(design, x, y);
					if (placable.Count > 0)
					{
						var part = placable[Random.Range(0, placable.Count)];
						if (Random.Range(0f, 1f) <= thresh && part.CanPlace(design, x, y))
							part.Place(design, x, y);
					}
				}
			}

			return design;
		}
	}
}
