using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Generator + "Level Builder")]
	public class LevelBuilder : TileObjectMapBuilder
	{
		public override GameObject Create(TileObjectMap map)
		{
			var design = map as LevelDesign;
			if (design == null)
				throw new System.ArgumentException("Map is not a LevelDesign.");

			var level = base.Create(map);

			level.transform.localScale = new Vector3(32, 32, 1);

			return level;
		}

		protected override void PlaceTiles(GameObject gameObject, TileObjectMap map, GameObject container)
		{
			var design = map as LevelDesign;

			var renderer = container.AddComponent<SpriteRenderer>();
			renderer.sprite = design.tileMapTexture.DrawSprite(map.Tiles);
		}
	}
}
