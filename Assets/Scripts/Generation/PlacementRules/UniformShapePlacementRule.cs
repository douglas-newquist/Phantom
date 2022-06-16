using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "Uniform Tile Shapes")]
	public class UniformShapePlacementRule : PlacementRule
	{
		public TileShape shape;

		public override bool CanPlace(TileObjectSO obj, TileObjectMap map, int x, int y)
		{
			for (int xi = 0; xi < obj.Width; xi++)
			{
				for (int yi = 0; yi < obj.Height; yi++)
				{
					if (!map.InBounds(x + xi, y + yi))
						return false;
					if (map.Get(x + xi, y + yi).Item2.Occupied)
						return false;
					if (!shape.HasFlag(map.Tiles.Get(x + xi, y + yi).Shape()))
						return false;
				}
			}

			return true;
		}
	}
}
