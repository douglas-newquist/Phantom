using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Phantom.StatSystem;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Part")]
	public class PartTile : MapTile
	{
		public StatSheetDefaults baseStats;

		public override void Place(Tilemap tilemap, Vector3Int position)
		{
			base.Place(tilemap, position);
			var root = tilemap.GetComponent<Transform>();
			var statSheet = root.GetComponentInParent<StatSheet>();

			if (statSheet != null)
			{
				baseStats.Apply(statSheet);
			}
		}
	}
}
