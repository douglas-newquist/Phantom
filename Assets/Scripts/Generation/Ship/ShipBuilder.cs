using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom
{
	/// <summary>
	/// Creates a GameObject from a ShipDesign
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu.Generator + "Ship Builder")]
	public class ShipBuilder : TileLayerMapBuilder<ShipDesign>
	{
		public override GameObject Create(ShipDesign design)
		{
			if (design == null)
				throw new System.ArgumentNullException("design");

			var obj = CreatePrefab();
			var tilemap = obj.GetComponentInChildren<Tilemap>();
			design.TileLayerMap.AddTiles(obj, tilemap);
			tilemap.transform.parent.position -= (Vector3)design.Bounds.center;
			return obj;
		}

		public GameObject Create(ShipGenerator generator, int width, int height)
		{
			if (generator == null)
				throw new System.ArgumentNullException("generator");

			return Create(generator.Create(width, height));
		}

		public GameObject Create(ShipGenerator generator)
		{
			if (generator == null)
				throw new System.ArgumentNullException("generator");

			return Create(generator.Create());
		}

		/// <summary>
		/// Creates a GameObject and registers it with the ObjectPooling system
		/// </summary>
		public string CreateRegister(ShipDesign design)
		{
			if (design == null)
				throw new System.ArgumentNullException("design");

			var ship = Create(design);
			ObjectPool.Register(design.Name, ship);
			return design.Name;
		}
	}
}
