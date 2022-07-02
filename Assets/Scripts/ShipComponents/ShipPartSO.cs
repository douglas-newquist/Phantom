using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Phantom.StatSystem;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.ShipPart + "Part")]
	public class ShipPartSO : MapTile
	{
		public StatSheetDefaults baseStats;

		public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			if (!base.StartUp(position, tilemap, go))
				return false;

			var root = tilemap.GetComponent<Transform>();
			var statSheet = root.GetComponentInParent<StatSheet>();

			if (statSheet != null)
			{
				baseStats.Apply(statSheet);
			}

			return true;
		}
	}
}
