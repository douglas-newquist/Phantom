using UnityEngine;

namespace Phantom
{
	/// <summary>
	/// Applies a tile generator
	/// </summary>
	[CreateAssetMenu(menuName = CreateMenu.TileObjectMapGenerator + "Apply Tile Generator")]
	public class ApplyTileGeneratorTileObjectMapGenerator : TileObjectMapGenerator
	{
		public override TileObjectMap ApplyOnce(TileObjectMap design, RectInt area)
		{
			design = (TileObjectMap)design.Clone();
			design.Tiles = tileGenerator.Apply(design.Tiles, area);
			return design;
		}
	}
}
