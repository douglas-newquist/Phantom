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
					var part = parts.parts[Random.Range(0, parts.parts.Length)];
					if (Random.Range(0f, 1f) <= thresh && part.CanPlace(design, x, y))
						part.Place(design, x, y);
				}
			}

			return design;
		}
	}
}
