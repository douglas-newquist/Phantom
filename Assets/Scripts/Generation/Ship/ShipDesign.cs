using UnityEngine;

namespace Phantom
{
	[System.Serializable]
	public class ShipDesign
	{
		public TileMapSO hullType;

		public TileMap tiles;

		public Grid2D<ShipPart> parts;

		public int Width => tiles.Width;

		public int Height => tiles.Height;

		public RectInt BoundingBox => tiles.BoundingBox;

		public ShipDesign(int width, int height)
		{
			tiles = new TileMap(width, height);
			parts = new Grid2D<ShipPart>(width, height);
		}

		public ShipDesign(ShipDesign design)
		{
			tiles = new TileMap(design.tiles);
			parts = new Grid2D<ShipPart>(design.parts);
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
			var sprite = hullType.GetSprite(tiles);
			var hull = new GameObject("Hull");
			hull.transform.SetParent(ship.transform);
			var renderer = hull.AddComponent<SpriteRenderer>();
			renderer.sprite = sprite;
		}

		public void AddParts(GameObject ship)
		{
			var parts = new GameObject("Parts");
			parts.transform.SetParent(ship.transform);

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					var part = this.parts.Get(x, y);
					var placed = part.Place(ship, this, x, y);
					if (placed != null)
					{
						placed.transform.SetParent(parts.transform);
						placed.transform.localPosition = new Vector3(x, y, 0);
					}
				}
			}
		}
	}
}
