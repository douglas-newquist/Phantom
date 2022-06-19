using UnityEngine;

namespace Phantom
{
	[CreateAssetMenu(menuName = CreateMenu.Generator + "Level Builder")]
	public class LevelBuilder : TileLayerMapBuilder<TileObjectMap>
	{
		public override GameObject Create(TileObjectMap map)
		{
			return null;
		}

	}
}
