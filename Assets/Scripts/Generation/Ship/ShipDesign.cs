using System;
using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Defines specific information on how to construct a ship
	/// </summary>
	[System.Serializable]
	public class ShipDesign : TileObjectMap
	{
		[SerializeField]
		private string name;

		public string Name { get => name; set => name = value; }

		public TileMapSO hullType;


		public ShipDesign(int width, int height) : base(width, height)
		{
		}

		public ShipDesign(TileObjectMap map) : base(map) { }

		public ShipDesign(ShipDesign design) : base(design)
		{
			hullType = design.hullType;
		}

		public GameObject Create(GameObject prefab)
		{
			var ship = GameObject.Instantiate(prefab);
			AddHull(ship);
			AddParts(ship);

			return ship;
		}

		public void AddHull(GameObject ship)
		{
			var sprite = hullType.GetSprite(Tiles);
			var hull = new GameObject("Hull");
			hull.transform.position -= (Vector3)BoundingBox.center;
			hull.transform.SetParent(ship.transform);
			var renderer = hull.AddComponent<SpriteRenderer>();
			renderer.sprite = sprite;

			var stats = ship.GetComponent<StatSheet>();
			hullType.ApplyStats(stats, Tiles);
		}

		public void AddParts(GameObject ship)
		{
			var parts = new GameObject("Parts");
			parts.transform.SetParent(ship.transform);
			parts.transform.position -= (Vector3)BoundingBox.center;

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					var part = Get(x, y).Item2;
					if (part.State == Reservation.Used)
					{
						var placed = part.Object.Place(ship, this, x, y);
						if (placed != null)
						{
							placed.transform.SetParent(parts.transform);
							placed.transform.localPosition = new Vector3(x, y, 0);
						}
					}
				}
			}
		}

		public override IGrid2D<Tuple<Tile, TileObject>> Clone()
		{
			return new ShipDesign(this);
		}
	}
}
