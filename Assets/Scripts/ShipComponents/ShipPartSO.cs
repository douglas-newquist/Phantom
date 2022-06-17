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

		public override GameObject Place(GameObject obj, TileObjectMap map, int x, int y, Transform container)
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
				part.transform.SetParent(container);
				part.transform.localPosition = new Vector3(x, y, 0);
			}

			return part;
		}
	}
}
