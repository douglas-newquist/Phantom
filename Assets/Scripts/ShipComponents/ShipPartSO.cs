using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Phantom.StatSystem;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Part")]
	public class ShipPartSO : MapTile
	{
		[Range(1, 16)]
		public int width = 1, height = 1;

		public override int Width => width;

		public override int Height => height;

		public List<StatPair> baseStats;

		public Modifier[] modifiers;

		public ResourceUsage resourceUsage;

		public override void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			base.RefreshTile(position, tilemap);
		}

		public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			var success = base.StartUp(position, tilemap, go);

			var root = tilemap.GetComponent<Transform>();
			var statSheet = root.GetComponentInParent<StatSheet>();

			if (statSheet != null)
			{
				foreach (var stat in baseStats)
					stat.Apply(statSheet);

				foreach (var modifier in modifiers)
					modifier.Apply(statSheet, this);
			}

			return success;
		}
	}
}
