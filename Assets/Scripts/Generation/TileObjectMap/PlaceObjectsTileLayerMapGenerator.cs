using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.TileLayerMapGenerator + "Random Objects")]
	public class PlaceObjectsTileLayerMapGenerator : TileLayerMapGenerator
	{
		[MinMax(0, 1)]
		public FloatRange density = 0.5f;

		[MinMax(-8, 8)]
		public IntRange zRange = new IntRange(0, 0);

		public TileObjectSO[] objects;

		public override TileLayerMap ApplyOnce(TileLayerMap design, RectInt area)
		{
			design = new TileLayerMap(design);
			float thresh = density.Random;

			for (int x = area.xMin; x < area.xMax; x++)
			{
				for (int y = area.yMin; y < area.yMax; y++)
				{
					for (int z = zRange.Min; z <= zRange.Max; z++)
					{
						var position = new Vector3Int(x, y, z);
						var placable = GetPlaceable(design, position);
						if (placable.Count > 0)
						{
							var part = placable[Random.Range(0, placable.Count)];
							if (Random.Range(0f, 1f) <= thresh && part.CanPlace(design, position))
								part.Place(design, position);
						}
					}
				}
			}

			return design;
		}

		public List<TileObjectSO> GetPlaceable(TileLayerMap design, Vector3Int position)
		{
			var placable = new List<TileObjectSO>();

			foreach (var obj in objects)
				if (obj.CanPlace(design, position))
					placable.Add(obj);

			return placable;
		}
	}
}
