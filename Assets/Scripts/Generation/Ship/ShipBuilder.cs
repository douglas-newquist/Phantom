using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Creates a GameObject from a ShipDesign
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu.Generator + "Ship Builder")]
	public class ShipBuilder : ScriptableObject
	{
		/*
		public override GameObject Create(TileObjectMap map)
		{
			var design = map as ShipDesign;
			if (design == null)
				throw new System.ArgumentException("Map is not a ShipDesign.");

			var ship = base.Create(map);

			return ship;
		}

		public GameObject Create(TileLayerMapGenerator generator, int width, int height)
		{
			return Create(generator.Create(width, height));
		}

		protected override void PlaceTileObjects(GameObject gameObject, TileObjectMap map, GameObject container)
		{
			var design = map as ShipDesign;

			base.PlaceTileObjects(gameObject, map, container);
			container.transform.position -= (Vector3)design.BoundingBox.center;
		}

		protected override void PlaceTiles(GameObject gameObject, TileObjectMap map, GameObject container)
		{
			var design = map as ShipDesign;

			container.transform.position -= (Vector3)design.BoundingBox.center;
			container.transform.SetParent(gameObject.transform);

			var sprite = design.HullType.GetSprite(design.Tiles);
			var renderer = container.AddComponent<SpriteRenderer>();
			renderer.sprite = sprite;

			var stats = gameObject.GetComponent<StatSheet>();
			design.HullType.ApplyStats(stats, design.Tiles);
		}*/
	}
}
