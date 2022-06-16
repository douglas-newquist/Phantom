using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.TileObjectMapGenerator + "Random Objects")]
	public class PlaceObjectsTileObjectMapGenerator : TileObjectMapGenerator
	{
		[MinMax(0, 1)]
		public FloatRange density = 0.5f;

		public TileObjectSO[] objects;

		public override TileObjectMap ApplyOnce(TileObjectMap design, RectInt area)
		{
			design = (TileObjectMap)design.Clone();
			float thresh = density.Random;

			for (int x = area.xMin; x < area.xMax; x++)
			{
				for (int y = area.yMin; y < area.yMax; y++)
				{
					var placable = GetPlaceable(design, x, y);
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

		public List<TileObjectSO> GetPlaceable(TileObjectMap design, int x, int y)
		{
			var placable = new List<TileObjectSO>();

			foreach (var obj in objects)
				if (obj.CanPlace(design, x, y))
					placable.Add(obj);

			return placable;
		}
	}
}
