using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Generator + "Level Builder")]
	public class LevelBuilder : TileLayerMapBuilder<LevelDesign>
	{
		public override GameObject Create(LevelDesign map)
		{
			var obj = CreatePrefab();
			var tilemap = obj.GetComponentInChildren<Tilemap>();
			map.TileLayerMap.AddTiles(obj, tilemap);

			var level = obj.GetComponent<Level>();
			level.LevelDesign = map;
			return obj;
		}

	}
}
