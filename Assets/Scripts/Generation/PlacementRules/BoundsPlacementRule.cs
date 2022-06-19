using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.PlacementRule + "In Bounds")]
	public class BoundsPlacementRule : PlacementRule
	{
		public bool inBounds = true;

		public override bool CanPlace(TileObjectSO obj, TileLayerMap map, Vector3Int position)
		{
			for (int xi = 0; xi < obj.Width; xi++)
				for (int yi = 0; yi < obj.Height; yi++)
					if (map.InBounds(position.x + xi, position.y + yi) != inBounds)
						return false;

			return true;
		}
	}
}
