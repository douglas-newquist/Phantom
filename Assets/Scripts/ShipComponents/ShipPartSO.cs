using System.Collections.Generic;
using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Part")]
	public class ShipPartSO : TileObjectSO
	{
		public GameObject prefab;

		public Sprite sprite;

		[Range(1, 16)]
		public int width = 1, height = 1;

		public override int Width => width;

		public override int Height => height;

		public List<StatPair> baseStats;

		public Modifier[] modifiers;

		public ResourceUsage resourceUsage;

		public override GameObject Place(GameObject obj, TileObjectMap map, int x, int y)
		{
			GameObject part = null;
			var statSheet = obj.GetComponent<StatSheet>();

			if (statSheet == null)
				throw new System.ArgumentNullException("Object has no stat sheet");

			foreach (var stat in baseStats)
				stat.Apply(statSheet);

			foreach (var modifier in modifiers)
				modifier.Apply(statSheet, this);

			if (prefab != null)
			{
				part = Instantiate(prefab);
			}

			return part;
		}

		public override void Place(TileObjectMap map, int x, int y)
		{
			map.Get(x, y).Item2.Object = this;

			for (int xi = 0; xi < width; xi++)
				for (int yi = 0; yi < height; yi++)
					if (xi != 0 || yi != 0)
						map.Get(x + xi, y + yi).Item2.Reference = new Vector2Int(x, y);
		}
	}
}
