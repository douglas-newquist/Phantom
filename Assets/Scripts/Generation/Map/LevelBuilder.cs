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
			obj.name = map.Name;

			var tilemap = obj.GetComponentInChildren<Tilemap>();
			map.TileLayerMap.AddTiles(obj, tilemap);
			tilemap.RefreshAllTiles();

			var level = obj.GetComponent<Level>();
			level.LevelDesign = map;

			var te = obj.GetComponentInChildren<TilemapCollider2D>();
			te.ProcessTilemapChanges();

			if (obj.TryGetComponent<CompositeCollider2D>(out var collider2D))
				collider2D.GenerateGeometry();

			return obj;
		}
	}
}
